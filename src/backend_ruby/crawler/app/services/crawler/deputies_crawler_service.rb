require 'httparty'

class Crawler::DeputiesCrawlerService
  API_URL = "https://dadosabertos.camara.leg.br/api/v2/deputados?ordem=ASC&ordenarPor=nome"

  def self.fetch_and_save
    cache_key = "all_deputies"

    if Rails.cache.exist?(cache_key)
      puts "Fetching data from cache for all deputies..."
      response_data = Rails.cache.read(cache_key)
    else
      puts "Fetching data from API for all deputies..."
      api_response = HTTParty.get(API_URL)

      # Only cache the response if the API call is successful and the body is not empty.
      if api_response.code == 200 && !(api_response.body.nil? || api_response.body.empty?)
        response_data = {
          code: api_response.code,
          body: api_response.body,
          parsed_response: api_response.parsed_response
        }
        Rails.cache.write(cache_key, response_data, expires_in: 30.days)
      else
        response_data = {
          code: api_response.code,
          body: api_response.body,
          parsed_response: api_response.parsed_response
        }
      end
    end

    # Explicitly check for an empty or nil body.
    if response_data[:body].nil? || response_data[:body].empty?
      puts "Warning: Received an empty response body for the deputies API."
      return
    end

    unless response_data[:code] == 200
      puts "Warning: Deputies API returned status #{response_data[:code]}"
      return
    end

    data = response_data[:parsed_response]['dados']
    data.each do |item|
      Deputy.find_or_create_by(external_id: item['id']) do |dep|
        dep.uri            = item['uri']
        dep.name           = item['nome']
        dep.sigla_partido  = item['siglaPartido']
        dep.uri_partido    = item['uriPartido']
        dep.sigla_uf       = item['siglaUf']
        dep.id_legislatura = item['idLegislatura']
        dep.url_foto       = item['urlFoto']
        dep.email          = item['email']
      end
    end
  end
end

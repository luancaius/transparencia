require 'httparty'

class Crawler::DeputiesCrawlerService
  API_URL = "https://dadosabertos.camara.leg.br/api/v2/deputados?ordem=ASC&ordenarPor=nome"

  def self.fetch_and_save
    response = Rails.cache.fetch("all_deputies", expires_in: 12.hours) do
      HTTParty.get(API_URL)
    end

    unless response.code == 200
      puts "Warning: Deputies API returned status #{response.code}"
      return
    end

    data = response.parsed_response['dados']
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

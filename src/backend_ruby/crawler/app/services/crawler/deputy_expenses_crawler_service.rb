require 'httparty'

class Crawler::DeputyExpensesCrawlerService
  def self.fetch_and_save(deputy_id, year, month)
    url = "https://dadosabertos.camara.leg.br/api/v2/deputados/#{deputy_id}/despesas?ano=#{year}&mes=#{month}&itens=10000"
    cache_key = "deputy_expenses_#{deputy_id}_#{year}_#{month}"

    if Rails.cache.exist?(cache_key)
      puts "Fetching data from cache for deputy #{deputy_id}, year #{year}, month #{month}..."
      response_data = Rails.cache.read(cache_key)
    else
      puts "Fetching data from API for deputy #{deputy_id}, year #{year}, month #{month}..."
      api_response = HTTParty.get(url)

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

    if response_data[:body].nil? || response_data[:body].empty?
      puts "Warning: Received an empty response body for deputy #{deputy_id}, year #{year}, month #{month}."
      return
    end

    unless response_data[:code] == 200
      puts "Warning: Expenses API returned status #{response_data[:code]} for deputy #{deputy_id}, year #{year}, month #{month}."
      return
    end

    data = response_data[:parsed_response]['dados']
    data.each do |item|
      Expense.find_or_create_by(cod_documento: item['codDocumento']) do |expense|
        expense.year                = item['ano']
        expense.month               = item['mes']
        expense.expense_type        = item['tipoDespesa']
        expense.document_type       = item['tipoDocumento']
        expense.cod_tipo_documento  = item['codTipoDocumento']
        expense.data_documento      = item['dataDocumento']
        expense.num_documento       = item['numDocumento']
        expense.valor_documento     = item['valorDocumento']
        expense.url_documento       = item['urlDocumento']
        expense.nome_fornecedor     = item['nomeFornecedor']
        expense.cnpj_cpf_fornecedor = item['cnpjCpfFornecedor']
        expense.valor_liquido       = item['valorLiquido']
        expense.valor_glosa         = item['valorGlosa']
        expense.num_ressarcimento   = item['numRessarcimento']
        expense.cod_lote            = item['codLote']
        expense.parcela             = item['parcela']
      end
    end
  end
end

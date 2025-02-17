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

      # Prepare response_data
      response_data = {
        code: api_response.code,
        body: api_response.body,
        parsed_response: api_response.parsed_response
      }

      # Cache only if 200 and body is not empty
      if api_response.code == 200 && !(api_response.body.nil? || api_response.body.empty?)
        Rails.cache.write(cache_key, response_data, expires_in: 30.days)
      end
    end

    # 1) Check if body is empty
    if response_data[:body].nil? || response_data[:body].empty?
      puts "Warning: Received an empty response body for deputy #{deputy_id}, year #{year}, month #{month}."
      return :empty_data
    end

    # 2) Check HTTP code
    unless response_data[:code] == 200
      puts "Warning: Expenses API returned status #{response_data[:code]} for deputy #{deputy_id}, year #{year}, month #{month}."
      return :api_error
    end

    # 3) Check if 'dados' is present and non-empty
    parsed_data = response_data[:parsed_response]
    if parsed_data.nil? || parsed_data['dados'].nil? || parsed_data['dados'].empty?
      puts "Warning: Received a 200 status code but 'dados' is empty for deputy #{deputy_id}, year #{year}, month #{month}."
      return :empty_data
    end

    # 4) If we get here, parse and save data
    data = parsed_data['dados']
    data.each do |item|
      Expense.find_or_create_by(cod_documento: item['codDocumento']) do |expense|
        expense.deputy_external_id    = deputy_id
        expense.year                  = item['ano']
        expense.month                 = item['mes']
        expense.expense_type          = item['tipoDespesa']
        expense.document_type         = item['tipoDocumento']
        expense.cod_tipo_documento    = item['codTipoDocumento']
        expense.data_documento        = item['dataDocumento']
        expense.num_documento         = item['numDocumento']
        expense.valor_documento       = item['valorDocumento'].to_f
        expense.url_documento         = item['urlDocumento']
        expense.nome_fornecedor       = item['nomeFornecedor']
        expense.cnpj_cpf_fornecedor   = item['cnpjCpfFornecedor']
        expense.valor_liquido         = item['valorLiquido'].to_f
        expense.valor_glosa           = item['valorGlosa'].to_f
        expense.num_ressarcimento     = item['numRessarcimento']
        expense.cod_lote              = item['codLote']
        expense.parcela               = item['parcela']
      end
    end

    # Return success since we got valid data and saved it
    :success
  end
end

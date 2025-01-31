require 'httparty'

class Crawler::DeputyExpensesCrawlerService
  def self.fetch_and_save(deputy_id, year, month)
    url = "https://dadosabertos.camara.leg.br/api/v2/deputados/#{deputy_id}/despesas?ano=#{year}&mes=#{month}&itens=10000"
    response = Rails.cache.fetch("deputy_expenses_#{deputy_id}_#{year}_#{month}", expires_in: 12.hours) do
      HTTParty.get(url)
    end

    unless response.code == 200
      puts "Warning: Expenses API returned status #{response.code} for deputy #{deputy_id}, year #{year}, month #{month}."
      return
    end

    data = response.parsed_response['dados']
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

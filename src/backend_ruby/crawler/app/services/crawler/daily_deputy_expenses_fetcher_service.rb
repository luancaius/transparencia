class Crawler::DailyDeputyExpensesFetcherService
  def self.call
    current_year  = Time.now.year
    current_month = Time.now.month

    Deputy.all.each do |dep|
      url = "https://dadosabertos.camara.leg.br/api/v2/deputados/#{dep.external_id}/despesas?ano=#{current_year}&mes=#{current_month}&itens=10000"
      puts "Fetching daily data for deputy #{dep.name} (ID: #{dep.external_id}) for Month #{current_month}..."

      api_response = HTTParty.get(url)
      if api_response.code != 200
        puts "  -> API error: received code #{api_response.code}. Skipping current month."
        next
      end

      parsed_data = api_response.parsed_response
      if parsed_data.nil? || parsed_data['dados'].nil? || parsed_data['dados'].empty?
        puts "  -> No data found for Month #{current_month}."
        next
      end

      # Sort the expenses in chronological order by document date.
      expenses_sorted = parsed_data['dados'].sort_by do |item|
        # Ensure proper parsing; in case of error fallback to Time.now
        DateTime.parse(item['dataDocumento']) rescue Time.now
      end

      expenses_sorted.each do |item|
        # Save or update the expense record to ensure all data is chronologically saved.
        expense = Expense.find_or_create_by(cod_documento: item['codDocumento'],
                                            deputy_external_id: dep.external_id) do |expense|
          expense.year                = item['ano']
          expense.month               = item['mes']
          expense.expense_type        = item['tipoDespesa']
          expense.document_type       = item['tipoDocumento']
          expense.cod_tipo_documento  = item['codTipoDocumento']
          expense.data_documento      = item['dataDocumento']
          expense.num_documento       = item['numDocumento']
          expense.valor_documento     = item['valorDocumento'].to_f
          expense.url_documento       = item['urlDocumento']
          expense.nome_fornecedor     = item['nomeFornecedor']
          expense.cnpj_cpf_fornecedor = item['cnpjCpfFornecedor']
          expense.valor_liquido       = item['valorLiquido'].to_f
          expense.valor_glosa         = item['valorGlosa'].to_f
          expense.num_ressarcimento   = item['numRessarcimento']
          expense.cod_lote            = item['codLote']
          expense.parcela             = item['parcela']
        end

        # Compare to determine the top expense ("winner") of the month.
        new_expense_value = item['valorDocumento'].to_f
        top_expense = TopExpenseMonth.where(year: current_year, month: current_month).first

        if top_expense.nil? || new_expense_value > top_expense.expense_value
          if top_expense.nil?
            TopExpenseMonth.create!(
              year:               current_year,
              month:              current_month,
              deputy_external_id: dep.external_id,
              expense_value:      new_expense_value,
              cod_documento:      item['codDocumento'],
              expense_type:       item['tipoDespesa']
            )
            puts "  -> New top expense created for Month #{current_month}: cod_documento #{item['codDocumento']} (#{new_expense_value})"
          else
            top_expense.update!(
              deputy_external_id: dep.external_id,
              expense_value:      new_expense_value,
              cod_documento:      item['codDocumento'],
              expense_type:       item['tipoDespesa']
            )
            puts "  -> Top expense updated for Month #{current_month}: cod_documento #{item['codDocumento']} (#{new_expense_value})"
          end
        end
      end
    end
    puts "Daily fetching complete for current month (#{current_month}/#{current_year})."
  end
end

class Crawler::DailyDeputyExpensesFetcherService
  def self.call(year)
    # Loop through all deputies
    Deputy.all.each do |dep|
      # For current year, only process until the current month; otherwise, process all 12 months.
      last_month = (year == Time.now.year ? Time.now.month : 12)

      (1..last_month).each do |month|
        url = "https://dadosabertos.camara.leg.br/api/v2/deputados/#{dep.external_id}/despesas?ano=#{year}&mes=#{month}&itens=10000"
        puts "Fetching daily data for deputy #{dep.name} (ID: #{dep.external_id}) for Month #{month}..."

        api_response = HTTParty.get(url)

        # Check for valid response
        if api_response.code != 200
          puts "  -> API error: received code #{api_response.code}. Skipping month #{month}."
          next
        end

        parsed_data = api_response.parsed_response
        if parsed_data.nil? || parsed_data['dados'].nil? || parsed_data['dados'].empty?
          puts "  -> No data found for Month #{month}."
          next
        end

        # Process each expense item
        parsed_data['dados'].each do |item|
          # Use cod_documento as a unique key to identify the expense record.
          # You might want to also use deputy_external_id (and perhaps year/month) for extra safety.
          expense_exists = Expense.where(cod_documento: item['codDocumento'],
                                         deputy_external_id: dep.external_id).exists?

          new_expense_exists = NewExpense.where(cod_documento: item['codDocumento'],
                                                deputy_external_id: dep.external_id).exists?

          if expense_exists
            # Already in final table
            next
          elsif new_expense_exists
            # Already flagged as new
            next
          else
            # This is new data; save it to NewExpense for manual review.
            NewExpense.create!(
              deputy_external_id: dep.external_id,
              year:                item['ano'],
              month:               item['mes'],
              expense_type:        item['tipoDespesa'],
              cod_documento:       item['codDocumento'],
              document_type:       item['tipoDocumento'],
              cod_tipo_documento:  item['codTipoDocumento'],
              data_documento:      item['dataDocumento'],
              num_documento:       item['numDocumento'],
              valor_documento:     item['valorDocumento'].to_f,
              url_documento:       item['urlDocumento'],
              nome_fornecedor:     item['nomeFornecedor'],
              cnpj_cpf_fornecedor: item['cnpjCpfFornecedor'],
              valor_liquido:       item['valorLiquido'].to_f,
              valor_glosa:         item['valorGlosa'].to_f,
              num_ressarcimento:   item['numRessarcimento'],
              cod_lote:            item['codLote'],
              parcela:             item['parcela']
            )
            puts "  -> New expense flagged: cod_documento #{item['codDocumento']}."
          end
        end
      end
    end
    puts "Daily fetching complete for year #{year}."
  end
end

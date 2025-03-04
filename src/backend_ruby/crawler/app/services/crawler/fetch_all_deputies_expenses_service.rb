class Crawler::FetchAllDeputiesExpensesService
  def self.call(year)
    # 1) Find or create a checkpoint for (checkpoint_type: 'expenses', year: year).
    checkpoint = Checkpoint.find_or_create_by(checkpoint_type: 'expenses', year: year)
    # Ensure 'deputies_completed' is an Array (if using a different structure, adapt accordingly)
    checkpoint.deputies_completed ||= []

    puts "Loaded checkpoint for year #{year}. Already completed deputies: #{checkpoint.deputies_completed.inspect}"

    # 2) Iterate over all deputies in the database
    Deputy.all.each do |dep|
      # Skip this deputy if already in checkpoint
      if checkpoint.deputies_completed.include?(dep.external_id)
        puts "Skipping deputy #{dep.name} (ID: #{dep.external_id}) for year #{year} — already processed."
        next
      end

      puts "Processing deputy #{dep.name} (ID: #{dep.external_id}) for year #{year}..."

      # Determine the last month to process: if year is the current year, only iterate until the current month; otherwise, use December.
      last_month = (year == Time.now.year ?  (Time.now.month - 1) : 12)
      if last_month < 1
        puts "  -> No previous month available for the current year. Skipping deputy #{dep.name}."
        next
      end
      # 3) Fetch expenses month by month
      (1..last_month).each do |month|
        puts "  Fetching expenses for Month #{month}..."
        result = Crawler::DeputyExpensesCrawlerService.fetch_and_save(dep.external_id, year, month)

        case result
        when :success
          # We got valid data for this month—keep going to the next month.
          puts "  -> Data fetched successfully for Month #{month}."
        when :empty_data
          # Data is empty; we can break or handle differently
          puts "  -> Empty data received for Month #{month}. Stopping month loop for this deputy."
          break
        when :api_error
          # Non-200 status, etc.
          puts "  -> API error on Month #{month}. Stopping month loop for this deputy."
          break
        else
          # If your fetch_and_save returns nil or some unexpected value
          puts "  -> Unexpected result (#{result.inspect}). Stopping month loop."
          break
        end
      end

      # 4) After all (or some) months, mark this deputy as completed
      checkpoint.deputies_completed << dep.external_id
      checkpoint.save!
      puts "Deputy #{dep.external_id} marked as completed for year #{year}."
    end

    puts "Finished processing all deputies for year #{year}."
  end
end

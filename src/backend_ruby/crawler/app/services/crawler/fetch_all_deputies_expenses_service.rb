class Crawler::FetchAllDeputiesExpensesService
  def self.call(year)
    # Load an existing checkpoint for the given year or create one if it doesn't exist.
    checkpoint = Checkpoint.where(checkpoint_type: 'expenses', year: year).first ||
      Checkpoint.create(checkpoint_type: 'expenses', year: year, deputies_completed: [])
    puts "Loaded checkpoint for year #{year}: deputies_completed=#{checkpoint.deputies_completed.inspect}"

    puts "Fetching all deputies..."
    Crawler::DeputiesCrawlerService.fetch_and_save

    # Iterate over all deputies
    Deputy.all.each do |dep|
      # If this deputy has been processed for the current year, skip it.
      if checkpoint.deputies_completed.include?(dep.external_id)
        puts "Skipping deputy #{dep.name} (ID: #{dep.external_id}) for year #{year} — already processed."
        next
      end

      # Process expenses for each month for the given year
      (1..12).each do |month|
        puts "Fetching expenses for Deputy #{dep.name}, ID: #{dep.external_id}, Year: #{year}, Month: #{month}"
        Crawler::DeputyExpensesCrawlerService.fetch_and_save(dep.external_id, year, month)
      end

      # After finishing the deputy for this year, update the checkpoint.
      checkpoint.deputies_completed << dep.external_id
      checkpoint.save
      puts "Updated checkpoint: Deputy #{dep.external_id} processed for year #{year}."
    end
  end
end

class Crawler::FetchAllDeputiesExpensesService
  def self.call(year)
    # Load an existing checkpoint or create one if it doesn't exist.
    checkpoint = Checkpoint.where(checkpoint_type: 'expenses').first ||
      Checkpoint.create(checkpoint_type: 'expenses', deputies_completed: [])
    puts "Loaded checkpoint: deputies_completed=#{checkpoint.deputies_completed.inspect}"

    puts "Fetching all deputies..."
    Crawler::DeputiesCrawlerService.fetch_and_save

    # Iterate over all deputies
    Deputy.all.each do |dep|
      # If this deputy has been processed already, skip it.
      if checkpoint.deputies_completed.include?(dep.external_id)
        puts "Skipping deputy #{dep.name} (ID: #{dep.external_id}) — already processed."
        next
      end

      # Process expenses for each month
      (1..12).each do |month|
        puts "Fetching expenses for Deputy #{dep.name}, ID: #{dep.external_id}, Year: #{year}, Month: #{month}"
        Crawler::DeputyExpensesCrawlerService.fetch_and_save(dep.external_id, year, month)
      end

      # After finishing the deputy, update the checkpoint.
      checkpoint.deputies_completed << dep.external_id
      checkpoint.save
      puts "Updated checkpoint: Deputy #{dep.external_id} processed."
    end
  end
end

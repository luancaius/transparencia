class Crawler::FetchAllDeputiesExpensesService
    def self.call(year)
      puts "Fetching all deputies..."
      Crawler::DeputiesCrawlerService.fetch_and_save
  
      Deputy.all.each do |dep|
        (1..12).each do |month|
          puts "Fetching expenses for Deputy #{dep.name}, ID: #{dep.external_id}, Year: #{year}, Month: #{month}"
          Crawler::DeputyExpensesCrawlerService.fetch_and_save(dep.external_id, year, month)
        end
      end
    end
  end
  
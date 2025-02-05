# services/monthly_file_generator_service.rb
require 'mongo'
require 'json'

class MonthlyFileGeneratorService
  def initialize(db_url: 'mongodb://127.0.0.1:27017/crawler_development', output_dir: './monthly_data')
    Mongo::Logger.logger.level = ::Logger::FATAL
    @db = Mongo::Client.new(db_url)
    @output_dir = output_dir
  end

  def generate(months, year)
    Dir.mkdir(@output_dir) unless Dir.exist?(@output_dir)
    months.each do |m|
      data = top_10_expenses_by_month(m, year)
      file_name = "top_expenses_#{year}-#{format('%02d', m)}.json"
      File.open(File.join(@output_dir, file_name), 'w') { |f| f.write(JSON.pretty_generate(data)) }
      puts "Generated file: #{file_name}, found #{data.size} expenses."
    end
  end

  private

  def top_10_expenses_by_month(month, year)
    query = { "year" => year, "month" => month }

    puts "-----"
    puts "DEBUG: Checking documents with query: #{query}"
    total_count = @db[:expenses].count_documents(query)
    puts "DEBUG: Found #{total_count} documents matching year=#{year}, month=#{month}"

    result = @db[:expenses].aggregate([
                                        { "$match": query },
                                        { "$sort": { "valor_documento" => -1 } },
                                        { "$limit": 10 }
                                      ]).to_a

    puts "DEBUG: Aggregation result size = #{result.size}"
    puts "-----"
    result
  end
end

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
    expenses = @db[:expenses].aggregate([
                                          { "$match": query },
                                          { "$sort": { "valor_documento": -1 } },
                                          { "$limit": 10 }
                                        ]).to_a

    # Fetch all deputy IDs
    deputy_ids = expenses.map { |e| e["deputy_external_id"] }.compact.uniq

    # Get all deputy docs at once (minimizes round trips)
    deputies_map = @db[:deputies]
                     .find({ "external_id" => { "$in" => deputy_ids } })
                     .map { |d| [d["external_id"], d] }
                     .to_h

    # Merge deputy name into the expense hash
    expenses.each do |exp|
      deputy = deputies_map[exp["deputy_external_id"]]
      exp["deputy_name"] = deputy["name"] if deputy
      exp["deputy_party"] = deputy["sigla_partido"] if deputy
    end

    expenses
  end
end

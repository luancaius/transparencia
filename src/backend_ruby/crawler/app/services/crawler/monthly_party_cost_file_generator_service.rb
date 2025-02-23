# services/monthly_party_cost_file_generator_service.rb
require 'mongo'
require 'json'

class MonthlyPartyCostFileGeneratorService
  def initialize(db_url: 'mongodb://127.0.0.1:27017/crawler_development', output_dir: './monthly_data')
    Mongo::Logger.logger.level = ::Logger::FATAL
    @db = Mongo::Client.new(db_url)
    @output_dir = output_dir
  end

  def generate(months, year)
    Dir.mkdir(@output_dir) unless Dir.exist?(@output_dir)
    months.each do |m|
      data = average_cost_by_party_by_month(m, year)
      file_name = "average_costs_by_party_#{year}-#{format('%02d', m)}.json"
      file_path = File.join(@output_dir, file_name)
      File.open(file_path, 'w') { |f| f.write(JSON.pretty_generate(data)) }
      puts "Generated file: #{file_name}, found #{data.size} party records."
    end
  end

  private

  def average_cost_by_party_by_month(month, year)
    query = { "year" => year, "month" => month }
    pipeline = [
      { "$match" => query },
      { "$lookup" => {
        "from"         => "deputies",
        "localField"   => "deputy_external_id",
        "foreignField" => "external_id",
        "as"           => "deputy_info"
      }
      },
      { "$unwind" => "$deputy_info" },
      { "$group" => {
        "_id" => {
          "party"  => "$deputy_info.sigla_partido",
          "deputy" => "$deputy_info.external_id"
        },
        "deputy_total" => { "$sum" => "$valor_documento" }
      }
      },
      { "$group" => {
        "_id" => "$_id.party",
        "average_spent" => { "$avg" => "$deputy_total" }
      }
      },
      { "$sort" => { "average_spent" => -1 } },
      { "$project" => {
        "party"         => "$_id",
        "average_spent" => 1,
        "_id"           => 0
      }
      }
    ]

    @db[:expenses].aggregate(pipeline).to_a
  end
end

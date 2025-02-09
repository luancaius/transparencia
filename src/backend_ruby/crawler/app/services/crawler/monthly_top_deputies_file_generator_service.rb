# services/monthly_top_deputies_file_generator_service.rb
require 'mongo'
require 'json'

class MonthlyTopDeputiesFileGeneratorService
  def initialize(db_url: 'mongodb://127.0.0.1:27017/crawler_development', output_dir: './monthly_data')
    Mongo::Logger.logger.level = ::Logger::FATAL
    @db = Mongo::Client.new(db_url)
    @output_dir = output_dir
  end

  def generate(months, year)
    Dir.mkdir(@output_dir) unless Dir.exist?(@output_dir)

    months.each do |m|
      data = top_10_deputies_by_month(m, year)
      file_name = "top_deputies_#{year}-#{format('%02d', m)}.json"
      file_path = File.join(@output_dir, file_name)
      File.open(file_path, 'w') { |f| f.write(JSON.pretty_generate(data)) }
      puts "Generated file: #{file_name}, found #{data.size} deputy records."
    end
  end

  private

  # This method groups the expenses by deputy, sums up their expense values,
  # and returns the top 10 deputies (sorted by total_spent) for the given month and year.
  # It also looks up additional deputy info (like complete name and party).
  def top_10_deputies_by_month(month, year)
    query = { "year" => year, "month" => month }
    puts "-----"
    puts "DEBUG: Checking expense documents with query: #{query}"
    total_count = @db[:expenses].count_documents(query)
    puts "DEBUG: Found #{total_count} expense documents for year=#{year}, month=#{month}"

    pipeline = [
      { "$match" => query },
      { "$group" => {
        "_id"        => "$deputy_external_id",    # Group by deputy identifier
        "total_spent" => { "$sum" => "$valor_documento" }
      }},
      { "$sort"  => { "total_spent" => -1 } },
      { "$limit" => 10 },
      # Look up additional deputy info from the deputies collection.
      { "$lookup" => {
        "from"         => "deputies",
        "localField"   => "_id",
        "foreignField" => "external_id",
        "as"           => "deputy_info"
      }},
      { "$unwind" => "$deputy_info" },
      { "$project" => {
        "deputy_id"   => "$_id",
        "deputy_name" => "$deputy_info.name",         # Complete name from deputy record
        "party"       => "$deputy_info.sigla_partido",  # Party abbreviation (or change as needed)
        "total_spent" => 1,
        "_id"         => 0
      }}
    ]

    result = @db[:expenses].aggregate(pipeline).to_a

    puts "DEBUG: Aggregation result size = #{result.size}"
    puts "-----"
    result
  end
end

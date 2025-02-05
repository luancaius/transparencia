# tasks/generate_monthly_files.rb
require_relative '../app/services/crawler/monthly_file_generator_service'

generator = MonthlyFileGeneratorService.new
generator.generate([1, 2], 2024)

# tasks/generate_monthly_files.rb
require_relative '../app/services/crawler/monthly_file_generator_service'

generator = MonthlyFileGeneratorService.new
generator.generate([1, 2, 3, 4,5,6,7,8,9,10,11,12], 2025)

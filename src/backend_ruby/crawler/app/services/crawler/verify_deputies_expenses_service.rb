class Crawler::VerifyDeputiesExpensesService
  def self.call(year)
    # 1) Find the checkpoint
    checkpoint = Checkpoint.find_by(checkpoint_type: 'expenses', year: year)
    unless checkpoint
      puts "No checkpoint found for expenses/#{year}."
      return
    end

    # 2) Ensure that deputies_completed is an array
    unless checkpoint.deputies_completed.is_a?(Array)
      puts "Checkpoint's deputies_completed is not an Array. Aborting."
      return
    end

    puts "Verifying data for year #{year}..."

    # Keep track of how many were removed because they're missing data
    missing_count = 0

    # We'll build a new array of valid deputies
    valid_deputies = []

    # 3) Check each deputy in the existing array
    checkpoint.deputies_completed.each do |deputy_id|
      # Query MongoDB for any expenses for this deputy & year
      expense_count = Expense.where(
        deputy_external_id: deputy_id,
        year: year
      ).count

      if expense_count > 0
        # 4) If there's data, keep the deputy in the checkpoint
        valid_deputies << deputy_id
      else
        # Otherwise, remove them
        puts "No data found in MongoDB for Deputy #{deputy_id}, Year #{year}. Removing from checkpoint."
        missing_count += 1
      end
    end

    # 5) Save the updated array back to the checkpoint
    checkpoint.deputies_completed = valid_deputies
    checkpoint.save!

    puts "Verification complete for year #{year}. Updated checkpoint saved."
    puts "Total missing deputies for year #{year}: #{missing_count}"
  end
end

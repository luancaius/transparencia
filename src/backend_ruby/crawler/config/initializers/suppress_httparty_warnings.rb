require 'warning'

# This will ignore any warnings that match the message about HTTParty overriding `response#nil?`
# and that come from a file in the ActiveSupport::Cache directory.
Warning.ignore(
  /#{Regexp.quote("HTTParty will no longer override `response#nil?`.")}.+#{Object.const_source_location('ActiveSupport::Cache')[0]}/m,
  )
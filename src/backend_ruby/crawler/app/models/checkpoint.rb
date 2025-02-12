# app/models/checkpoint.rb
class Checkpoint
  include Mongoid::Document
  include Mongoid::Timestamps

  # A field to distinguish this checkpoint from others (if needed)
  field :checkpoint_type, type: String, default: 'expenses'

  # An array of deputy external IDs that have been processed
  field :deputies_completed, type: Array, default: []
end

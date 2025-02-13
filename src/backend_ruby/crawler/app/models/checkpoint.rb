# app/models/checkpoint.rb
class Checkpoint
  include Mongoid::Document
  include Mongoid::Timestamps

  field :checkpoint_type, type: String, default: 'expenses'
  field :year,            type: Integer

  # An array of deputy external IDs that have been processed for this year
  field :deputies_completed, type: Array, default: []
end

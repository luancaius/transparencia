# app/models/top_expense_month.rb
class TopExpenseMonth
  include Mongoid::Document
  include Mongoid::Timestamps

  field :year,                type: Integer
  field :month,               type: Integer
  field :deputy_external_id,  type: Integer
  field :expense_value,       type: Float
  field :cod_documento,       type: Integer
  field :expense_type,        type: String
end

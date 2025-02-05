class Expense
    include Mongoid::Document
  
    field :year,                type: Integer
    field :month,               type: Integer
    field :expense_type,        type: String
    field :cod_documento,       type: Integer
    field :document_type,       type: String
    field :cod_tipo_documento,  type: Integer
    field :data_documento,      type: DateTime
    field :num_documento,       type: String
    field :valor_documento,     type: Float
    field :url_documento,       type: String
    field :nome_fornecedor,     type: String
    field :cnpj_cpf_fornecedor, type: String
    field :valor_liquido,       type: Float
    field :valor_glosa,         type: Float
    field :num_ressarcimento,   type: String
    field :cod_lote,            type: Integer
    field :parcela,             type: Integer
  end
  
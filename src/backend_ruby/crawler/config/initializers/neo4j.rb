# config/initializers/neo4j.rb
require 'neo4j_ruby_driver'

NEO4J_URI      = 'bolt://localhost:7687'
NEO4J_USER     = 'neo4j'
NEO4J_PASSWORD = 'test'

Neo4jDriver = Neo4j::Driver::GraphDatabase.driver(
  NEO4J_URI,
  Neo4j::Driver::AuthTokens.basic(NEO4J_USER, NEO4J_PASSWORD)
)

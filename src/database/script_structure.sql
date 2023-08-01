-- Criar o schema 'geral'
CREATE SCHEMA IF NOT EXISTS geral;
USE geral;

-- Tabela pais
CREATE TABLE IF NOT EXISTS geral.pais (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    populacao BIGINT
);

-- Tabela estado
CREATE TABLE IF NOT EXISTS geral.estado (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    sigla CHAR(2),
    populacao BIGINT
);

-- Tabela pessoa
CREATE TABLE IF NOT EXISTS geral.pessoa (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INT,
    endereco VARCHAR(200)
);

-- Tabela cidade
CREATE TABLE IF NOT EXISTS geral.cidade (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    populacao BIGINT,
    estado_id INT,
    FOREIGN KEY (estado_id) REFERENCES geral.estado(id)
);

-- Tabela empresa
CREATE TABLE IF NOT EXISTS geral.empresa (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cnpj VARCHAR(20) UNIQUE,
    endereco VARCHAR(200)
);


-- Criar o schema 'congresso'
CREATE SCHEMA IF NOT EXISTS congresso;
USE congresso;

-- Tabela partidos
CREATE TABLE IF NOT EXISTS congresso.partidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    sigla CHAR(10)
);

-- Tabela legislaturas
CREATE TABLE IF NOT EXISTS congresso.legislaturas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    numero INT,
    inicio DATE,
    fim DATE
);

-- Tabela deputados
CREATE TABLE IF NOT EXISTS congresso.deputados (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    partido_id INT,
    legislatura_id INT,
    FOREIGN KEY (partido_id) REFERENCES congresso.partidos(id),
    FOREIGN KEY (legislatura_id) REFERENCES congresso.legislaturas(id)
);

-- Tabela despesas
CREATE TABLE IF NOT EXISTS congresso.despesas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    deputado_id INT,
    valor DECIMAL(10, 2),
    descricao VARCHAR(200),
    data DATE,
    FOREIGN KEY (deputado_id) REFERENCES congresso.deputados(id)
);

-- Tabela sessoes
CREATE TABLE IF NOT EXISTS congresso.sessoes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    data DATE,
    descricao VARCHAR(200)
);

CREATE DATABASE loja;
USE loja;

CREATE TABLE usuario 
( 
 idusuario INT PRIMARY KEY AUTO_INCREMENT,  
 nome VARCHAR(30) NOT NULL,  
 email VARCHAR(50) NOT NULL,  
 senha VARCHAR(6) NOT NULL,
 tipo int NOT NULL default 0, 
 std int NOT NULL default 0,
 UNIQUE (email)
); 

CREATE TABLE formapagamento 
( 
 idformapagamento INT PRIMARY KEY AUTO_INCREMENT,  
 nomecompleto VARCHAR(50) NOT NULL,  
 numero double NOT NULL,  
 cvv CHAR(3) NOT NULL,  
 vencimento VARCHAR(5),  
 idusuario INT,
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 

CREATE TABLE endereco 
( 
 idendereco INT PRIMARY KEY AUTO_INCREMENT,  
 idusuario INT NOT NULL,  
 endereco VARCHAR(50) NOT NULL,  
 complemento VARCHAR(30) NOT NULL,  
 cep VARCHAR(8) NOT NULL,  
 telefone VARCHAR(11) NOT NULL,  
 responsavel BOOLEAN NOT NULL,  
 UNIQUE (telefone),
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 

CREATE TABLE marca 
( 
 idmarca INT PRIMARY KEY AUTO_INCREMENT,  
 nome_marca VARCHAR(30),  
 UNIQUE (nome_marca)
); 

CREATE TABLE modalidade 
( 
 idmodalidade INT PRIMARY KEY AUTO_INCREMENT,  
 nome_modalidade VARCHAR(30) NOT NULL,  
 UNIQUE (nome_modalidade)
); 

CREATE TABLE produto 
( 
 idproduto INT PRIMARY KEY AUTO_INCREMENT,  
 nome VARCHAR(30),  
 descricao VARCHAR(100),  
 lancamento DATE,  
 quantidade INT NOT NULL DEFAULT 0,  
 preco DECIMAL(10,2) NOT NULL,  
 sts INT NOT NULL,  
 marca INT NOT NULL,  
 nacional BOOLEAN NOT NULL,  
 idmodalidade INT NOT NULL,
 FOREIGN KEY(marca) REFERENCES marca (idmarca),
 FOREIGN KEY(idmodalidade) REFERENCES modalidade (idmodalidade)
); 

CREATE TABLE itens_pedido 
( 
 iditem INT PRIMARY KEY AUTO_INCREMENT,  
 idusuario INT NOT NULL,
 nomeproduto VARCHAR(30) NOT NULL,
 quantidade INT NOT NULL,  
 preco_unitario DECIMAL(10,2) NOT NULL,
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 

CREATE TABLE pedido 
( 
 idpedido INT PRIMARY KEY AUTO_INCREMENT,  
 valor_total DECIMAL(10,2) NOT NULL,  
 idusuario INT NOT NULL,  
 sts INT NOT NULL,
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 




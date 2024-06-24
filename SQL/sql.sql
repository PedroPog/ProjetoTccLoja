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

INSERT INTO usuario (nome, email, senha, tipo, std)
        VALUES ("adm","adm@gmail.com","123456",1,1);
INSERT INTO usuario (nome, email, senha, tipo, std)
        VALUES ("cliente","cliente@gmail.com","123456",0,1);

CREATE TABLE formapagamento 
( 
 idformapagamento INT PRIMARY KEY AUTO_INCREMENT,  
 nomecompleto VARCHAR(50) NOT NULL,  
 numero VARCHAR(16) NOT NULL,  
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
 lancamento VARCHAR(10),  
 quantidade INT NOT NULL DEFAULT 0,  
 preco double NOT NULL,  
 sts BOOLEAN NOT NULL,  
 idmarca INT NOT NULL,  
 nacional BOOLEAN NOT NULL,  
 idmodalidade INT NOT NULL,
 FOREIGN KEY(idmarca) REFERENCES marca (idmarca),
 FOREIGN KEY(idmodalidade) REFERENCES modalidade (idmodalidade)
); 

CREATE TABLE imagens 
( 
 idimagem INT PRIMARY KEY AUTO_INCREMENT,
 idproduto INT not null,
 urlimagem VARCHAR(255) NOT NULL,  
 FOREIGN KEY(idproduto) REFERENCES produto (idproduto)
); 

INSERT INTO marca (nome_marca) VALUES ('Nike');
INSERT INTO marca (nome_marca) VALUES ('Adidas');
INSERT INTO marca (nome_marca) VALUES ('Puma');
INSERT INTO marca (nome_marca) VALUES ('Under Armour');

INSERT INTO modalidade (nome_modalidade) VALUES ('Futebol');
INSERT INTO modalidade (nome_modalidade) VALUES ('Basketball');
INSERT INTO modalidade (nome_modalidade) VALUES ('Vôlei');


CREATE TABLE pedido 
( 
 idpedido INT PRIMARY KEY AUTO_INCREMENT,  
 valor_total DOUBLE NOT NULL,  
 idusuario INT NOT NULL,  
 sts INT NOT NULL,
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 

CREATE TABLE itens_pedido 
( 
 iditem INT PRIMARY KEY AUTO_INCREMENT,  
 idusuario INT NOT NULL,
 idpedido INT NOT NULL,
 idproduto INT NOT NULL,
 nomeproduto VARCHAR(30) NOT NULL,
 quantidade INT NOT NULL,  
 preco_unitario DOUBLE NOT NULL,
 FOREIGN KEY(idpedido) REFERENCES pedido (idpedido),
 FOREIGN KEY(idproduto) REFERENCES produto (idproduto),
 FOREIGN KEY(idusuario) REFERENCES usuario (idusuario)
); 



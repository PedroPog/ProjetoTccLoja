INSERT INTO produto (nome, descricao, lancamento, quantidade, preco, sts, idmarca, nacional, idmodalidade)
        VALUES ("são paulo", "Camisa 01", "03/06/2024", 10, 300.00, true,1,
        true, 1);
    
INSERT INTO imagens(idproduto,urlimagem)
	VALUES(1,"sp1.1.png");
INSERT INTO imagens(idproduto,urlimagem)
	VALUES(1,"sp1.2.png");
INSERT INTO imagens(idproduto,urlimagem)
	VALUES(1,"sp1.3.png");
    
UPDATE produto
SET sts = true
WHERE idproduto = 1;


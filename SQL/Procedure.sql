/* PROCEDURRE */

DELIMITER $$

CREATE PROCEDURE GerenciarUsuario(
    IN operacao VARCHAR(10),
    IN id_usuario INT,
    IN nome_usuario VARCHAR(30),
    IN email_usuario VARCHAR(50),
    IN senha_usuario VARCHAR(6),
    IN tipo_usuario INT,
    IN std_usuario INT
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO usuario (nome, email, senha, tipo, std) VALUES (nome_usuario, email_usuario, senha_usuario, tipo_usuario, std_usuario);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE usuario
        SET nome = nome_usuario,
            email = email_usuario,
            senha = senha_usuario,
            tipo = tipo_usuario,
            std = std_usuario
        WHERE idusuario = id_usuario;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM usuario WHERE idusuario = id_usuario;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarUsuario('INSERT', null, 'Pedro', 'pedor@email.com', '123456', 1, 1);
CALL GerenciarUsuario('UPDATE', 1, 'Pedro Henrique', 'pedro@email.com', '123456', 2, 2);
CALL GerenciarUsuario('DELETE', 1, NULL, NULL, NULL, NULL, NULL);

DELIMITER $$

CREATE PROCEDURE GerenciarFormaPagamento(
    IN operacao VARCHAR(10),
    IN id_forma_pagamento INT,
    IN nome_completo VARCHAR(50),
    IN numero double,
    IN cvv CHAR(3),
    IN vencimento VARCHAR(5),
    IN id_usuario INT
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO formapagamento (nomecompleto, numero, cvv, vencimento, idusuario)
        VALUES (nome_completo, numero, cvv, vencimento, id_usuario);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE formapagamento
        SET nomecompleto = nome_completo,
            numero = numero,
            cvv = cvv,
            vencimento = vencimento,
            idusuario = id_usuario
        WHERE idformapagamento = id_forma_pagamento;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM formapagamento WHERE idformapagamento = id_forma_pagamento;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarFormaPagamento('INSERT', NULL, 'PEDRO HENRIQUE', 4021592347403027, '123', '12/24', 1);
CALL GerenciarFormaPagamento('UPDATE', 1, 'PEDRO HENRIQUE VIEIRA DE FREITAS', 1234567890123456, '123', '12/24', 1);
CALL GerenciarFormaPagamento('DELETE', 1, NULL, NULL, NULL, NULL, NULL);

DELIMITER $$

CREATE PROCEDURE GerenciarEndereco(
    IN operacao VARCHAR(10),
    IN id_endereco INT,
    IN id_usuario INT,
    IN endereco VARCHAR(50),
    IN complemento VARCHAR(30),
    IN cep VARCHAR(8),
    IN telefone VARCHAR(11),
    IN responsavel BOOLEAN
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO endereco (idusuario, endereco, complemento, cep, telefone, responsavel)
        VALUES (id_usuario, endereco, complemento, cep, telefone, responsavel);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE endereco
        SET idusuario = id_usuario,
            endereco = endereco,
            complemento = complemento,
            cep = cep,
            telefone = telefone,
            responsavel = responsavel
        WHERE idendereco = id_endereco;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM endereco WHERE idendereco = id_endereco;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarEndereco('INSERT', NULL, 1, 'Rua Principal', 'Apto 101', '12345678', '11987654321', TRUE);
CALL GerenciarEndereco('UPDATE', 1, 1, 'Rua Atualizada', 'Apto 202', '12345678', '11987654321', TRUE);
CALL GerenciarEndereco('DELETE', 1, NULL, NULL, NULL, NULL, NULL, NULL);


DELIMITER $$

CREATE PROCEDURE GerenciarMarca(
    IN operacao VARCHAR(10),
    IN id_marca INT,
    IN nome_marca VARCHAR(30)
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO marca (nome_marca) VALUES (nome_marca);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE marca SET nome_marca = nome_marca WHERE idmarca = id_marca;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM marca WHERE idmarca = id_marca;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarMarca('INSERT', NULL, 'Nova Marca');
CALL GerenciarMarca('UPDATE', 1, 'Marca Atualizada');
CALL GerenciarMarca('DELETE', 1, NULL);

DELIMITER $$

CREATE PROCEDURE GerenciarModalidade(
    IN operacao VARCHAR(10),
    IN id_modalidade INT,
    IN nome_modalidade VARCHAR(30)
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO modalidade (nome_modalidade) VALUES (nome_modalidade);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE modalidade SET nome_modalidade = nome_modalidade WHERE idmodalidade = id_modalidade;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM modalidade WHERE idmodalidade = id_modalidade;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarModalidade('INSERT', NULL, 'Nova Modalidade');
CALL GerenciarModalidade('UPDATE', 1, 'Modalidade Atualizada');
CALL GerenciarModalidade('DELETE', 1, NULL);


DELIMITER $$

CREATE PROCEDURE GerenciarProduto(
    IN operacao VARCHAR(10),
    IN id_produto INT,
    IN nome_produto VARCHAR(30),
    IN descricao_produto VARCHAR(100),
    IN lancamento_produto DATE,
    IN quantidade_produto INT,
    IN preco_produto DECIMAL(10,2),
    IN sts_produto INT,
    IN id_marca INT,
    IN nacional_produto BOOLEAN,
    IN id_modalidade INT
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO produto (nome, descricao, lancamento, quantidade, preco, sts, marca, nacional, idmodalidade)
        VALUES (nome_produto, descricao_produto, lancamento_produto, quantidade_produto, preco_produto, sts_produto, id_marca, nacional_produto, id_modalidade);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE produto
        SET nome = nome_produto,
            descricao = descricao_produto,
            lancamento = lancamento_produto,
            quantidade = quantidade_produto,
            preco = preco_produto,
            sts = sts_produto,
            marca = id_marca,
            nacional = nacional_produto,
            idmodalidade = id_modalidade
        WHERE idproduto = id_produto;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM produto WHERE idproduto = id_produto;
	elseif operacao = 'ESTOQUE' THEN
		UPDATE produto SET quantidade = quantidade_produto;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarProduto('INSERT', NULL, 'Nome do Produto', 'Descrição do Produto', '2024-04-30', 100, 50.00, 1, 1, TRUE, 1);
CALL GerenciarProduto('UPDATE', 1, 'Novo Nome', 'Nova Descrição', '2024-04-30', 200, 75.00, 2, 2, FALSE, 2);
CALL GerenciarProduto('ESTOQUE', 1,null, null, null, 200,null,null,null,null,null);
CALL GerenciarProduto('DELETE', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

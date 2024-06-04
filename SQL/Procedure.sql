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
/*CALL GerenciarUsuario('DELETE', 1, NULL, NULL, NULL, NULL, NULL);*/

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
/*CALL GerenciarFormaPagamento('DELETE', 1, NULL, NULL, NULL, NULL, NULL);*/

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
/*CALL GerenciarEndereco('DELETE', 1, NULL, NULL, NULL, NULL, NULL, NULL);*/


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
/*CALL GerenciarMarca('DELETE', 1, NULL);*/

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
/*CALL GerenciarModalidade('DELETE', 1, NULL);*/




DELIMITER $$

CREATE PROCEDURE GerenciarItensPedido(
    IN operacao VARCHAR(10),
    IN id_item INT,
    IN id_usuario INT,
    IN nome_produto VARCHAR(30),
    IN quantidade INT,
    IN preco_unitario DECIMAL(10,2)
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO itens_pedido (idusuario, nomeproduto, quantidade, preco_unitario)
        VALUES (id_usuario, nome_produto, quantidade, preco_unitario);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE itens_pedido
        SET idusuario = id_usuario,
            nomeproduto = nome_produto,
            quantidade = quantidade,
            preco_unitario = preco_unitario
        WHERE iditem = id_item;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM itens_pedido WHERE iditem = id_item;
    ELSEIF operacao = 'ESTOQUE' THEN
        UPDATE itens_pedido
        SET quantidade = quantidade
        WHERE iditem = id_item;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarItensPedido('INSERT', NULL, 1, 'Produto A', 10, 5.99);
CALL GerenciarItensPedido('UPDATE', 1, 1, 'Produto A Atualizado', 15, 6.99);
/*CALL GerenciarItensPedido('DELETE', 1, NULL, NULL, NULL, NULL);*/
CALL GerenciarItensPedido('ESTOQUE', 2, NULL, NULL, 20, NULL);

DELIMITER $$

CREATE PROCEDURE GerenciarPedido(
    IN operacao VARCHAR(10),
    IN id_pedido INT,
    IN valor_total DECIMAL(10,2),
    IN id_usuario INT,
    IN sts_pedido INT
)
BEGIN
    IF operacao = 'INSERT' THEN
        INSERT INTO pedido (valor_total, idusuario, sts)
        VALUES (valor_total, id_usuario, sts_pedido);
    ELSEIF operacao = 'UPDATE' THEN
        UPDATE pedido
        SET valor_total = valor_total,
            idusuario = id_usuario,
            sts = sts_pedido
        WHERE idpedido = id_pedido;
    ELSEIF operacao = 'DELETE' THEN
        DELETE FROM pedido WHERE idpedido = id_pedido;
    ELSE
        SELECT 'Operação inválida';
    END IF;
END $$

CALL GerenciarPedido('INSERT', NULL, 100.00, 1, 1);
CALL GerenciarPedido('UPDATE', 1, 150.00, 1, 2);
/*CALL GerenciarPedido('DELETE', 1, NULL, NULL, NULL);*/


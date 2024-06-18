select * from Usuario where idusuario = "1" and senha = "123456";
select * from Usuario where idusuario = "1";

INSERT INTO formapagamento (idformapagamento, nomecompleto, numero, cvv, vencimento, idusuario)
        VALUES (default, "Pedro Henrique Vieira", 1234567891023,"123", "10/23", 3);
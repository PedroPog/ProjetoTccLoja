use loja;

INSERT INTO pedido(idpedido,valor_total,idusuario,sts)
VALUES(default,0.0,2,1);

select * from pedido;
SELECT * FROM itens_pedido;


INSERT INTO itens_pedido(iditem, idusuario, idpedido,idproduto, nomeproduto, quantidade, preco_unitario)
values (default,2,1,1,'são paulo',10,300);


-- Criar a view
CREATE VIEW Produtos_View AS
SELECT p.idproduto, p.nome, p.descricao, p.lancamento, p.quantidade, p.preco, p.sts, m.nome_marca,
 p.nacional, mo.nome_modalidade
FROM produto p
INNER JOIN marca m ON p.idmarca = m.idmarca
INNER JOIN modalidade mo ON p.idmodalidade = mo.idmodalidade;


-- Exemplo de consulta usando a view para buscar todos os produtos
SELECT * FROM Produtos_View;

-- Exemplo de consulta usando a view para buscar produtos por nome
SELECT * FROM Produtos_View WHERE nome LIKE '%produto%';

-- Exemplo de consulta usando a view para buscar produtos por marca
SELECT * FROM Produtos_View WHERE nome_marca = 'NomeDaMarca';

-- Exemplo de consulta usando a view para buscar produtos por modalidade
SELECT * FROM Produtos_View WHERE nome_modalidade = 'NomeDaModalidade';

-- Exemplo de consulta usando a view para buscar produtos nacionais
SELECT * FROM Produtos_View WHERE nacional = TRUE;

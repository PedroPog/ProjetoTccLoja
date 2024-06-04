﻿using LojaCamisa.Models;
using LojaCamisa.Repository.Interface.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaCamisa.Repository.Interface
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _conexao;

        public ProdutoRepository(IConfiguration conf)
        {
            _conexao = conf.GetConnectionString("Conexao");
        }

        public Produtos ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();

                // Consulta principal para obter os detalhes do produto
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produtos_View WHERE idproduto=@idproduto;", conexao);
                cmd.Parameters.Add("@idproduto", MySqlDbType.Int32).Value = id;

                MySqlDataReader reader = cmd.ExecuteReader();

                Produtos produto = null;

                if (reader.Read())
                {
                    produto = new Produtos
                    {
                        idProduto = (int)reader["idproduto"],
                        nomeProduto = (string)reader["nome"],
                        descricao = (string)reader["descricao"],
                        lancamento = (string)reader["lancamento"],
                        quantidade = (int)reader["quantidade"],
                        preco = (double)reader["preco"],
                        status = (bool)reader["sts"],
                        marca = (string)reader["nome_marca"],
                        nacional = (bool)reader["nacional"],
                        modalidade = (string)reader["nome_modalidade"]
                    };
                }

                reader.Close();

                if (produto != null)
                {
                    // Consulta secundária para obter as URLs das imagens
                    cmd = new MySqlCommand("SELECT urlimagem FROM imagens WHERE idproduto=@idproduto;", conexao);
                    cmd.Parameters.Add("@idproduto", MySqlDbType.Int32).Value = id;

                    reader = cmd.ExecuteReader();

                    List<string> imagens = new List<string>();

                    while (reader.Read())
                    {
                        imagens.Add((string)reader["urlimagem"]);
                    }

                    reader.Close();

                    produto.imagem = imagens;
                }

                return produto;
            }
        }


        public IEnumerable<Produtos> ObterTodosProdutos()
        {
            List<Produtos> listProduto = new List<Produtos>();
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produtos_View WHERE sts = 1;", conexao);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable data = new DataTable();

                adapter.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    Produtos produto = new Produtos
                    {
                        idProduto = (int)dr["idproduto"],
                        nomeProduto = (string)dr["nome"],
                        descricao = (string)dr["descricao"],
                        lancamento = (string)dr["lancamento"],
                        quantidade = (int)dr["quantidade"],
                        preco = (double)dr["preco"],
                        status = (bool)dr["sts"],
                        marca = (string)dr["nome_marca"],
                        nacional = (bool)dr["nacional"],
                        modalidade = (string)dr["nome_modalidade"],
                        imagem = new List<string>()  // Inicializa a lista de imagens
                    };

                    // Consulta secundária para obter as URLs das imagens para cada produto
                    MySqlCommand imgCmd = new MySqlCommand("SELECT urlimagem FROM imagens WHERE idproduto=@idproduto;", conexao);
                    imgCmd.Parameters.Add("@idproduto", MySqlDbType.Int32).Value = produto.idProduto;
                    MySqlDataReader imgReader = imgCmd.ExecuteReader();

                    while (imgReader.Read())
                    {
                        produto.imagem.Add((string)imgReader["urlimagem"]);
                    }

                    imgReader.Close();

                    listProduto.Add(produto);
                }

                return listProduto;
            }
        }


        public void Atualizar(Produtos produtos)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEstoque(int Quantidade)
        {
            throw new NotImplementedException();
        }

        public void CadastrarProduto(Produtos produtos)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}
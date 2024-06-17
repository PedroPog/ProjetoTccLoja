using System.Data;
using LojaCamisa.Models;
using LojaCamisa.Repository.Interface.Contract;
using MySql.Data.MySqlClient;

namespace LojaCamisa.Repository.Interface;

public class ItensPedidoRepository : IItensPedidoRepository
{
    private readonly string _conexao;

    public ItensPedidoRepository(IConfiguration conf)
    {
        _conexao = conf.GetConnectionString("Conexao");
    }
    
    public IEnumerable<ItensPedido> ObterTodosPeditosAdm()
    {
        List<ItensPedido> listPedidos = new List<ItensPedido>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM itens_pedido;", conexao);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                ItensPedido itensPedido = new ItensPedido
                {
                    IdItem = (int)dr["iditem"],
                    IdUsuario = (int)dr["idusuario"],
                    NomeProduto = (string)dr["nomeproduto"],
                    Quantidade = (int)dr["quantidade"],
                    PrecoUni = (double)dr["preco_unitario"],
                };
                    
                listPedidos.Add(itensPedido);
            }

            return listPedidos;
        }
    }

    public IEnumerable<ItensPedido> ObterTodosPeditos(int IdUsuario)
    {
        List<ItensPedido> listPedidos = new List<ItensPedido>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM itens_pedido where idusuario=@idUsu;", conexao);
            cmd.Parameters.Add("@idUsu", MySqlDbType.Int32).Value = IdUsuario;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                ItensPedido itensPedido = new ItensPedido
                {
                    IdItem = (int)dr["iditem"],
                    IdUsuario = (int)dr["idusuario"],
                    NomeProduto = (string)dr["nomeproduto"],
                    Quantidade = (int)dr["quantidade"],
                    PrecoUni = (double)dr["preco_unitario"],
                };
                    
                listPedidos.Add(itensPedido);
            }

            return listPedidos;
        }
    }

    public ItensPedido ObterItemPedido(int IdItemPedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM itens_pedido WHERE iditem = @iditem;", conexao);

            cmd.Parameters.AddWithValue("@iditem", IdItemPedido);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new ItensPedido
                    {
                        IdItem = (int)reader["iditem"],
                        IdUsuario = (int)reader["idusuario"],
                        NomeProduto = (string)reader["nomeproduto"],
                        Quantidade = (int)reader["quantidade"],
                        PrecoUni = (double)reader["preco_unitario"]
                    };
                }
                else
                {
                    // Forma de pagamento não encontrada
                    return null;
                }
            }
        }
    }

    public void Cadastrar(ItensPedido itensPedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO itens_pedido (iditem,idusuario, nomeproduto, quantidade, preco_unitario) " +
                "VALUES (default,@idusuario, @nomeproduto, @quantidade, @preco_unitario);", conexao);

            cmd.Parameters.AddWithValue("@idusuario", itensPedido.IdUsuario);
            cmd.Parameters.AddWithValue("@nomeproduto", itensPedido.NomeProduto);
            cmd.Parameters.AddWithValue("@quantidade", itensPedido.Quantidade);
            cmd.Parameters.AddWithValue("@preco_unitario", itensPedido.PrecoUni);

            cmd.ExecuteNonQuery();
        }
    }

    public void Atualizar(ItensPedido itensPedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "UPDATE itens_pedido SET nomeproduto = @nomeproduto, " +
                "quantidade = @quantidade, preco_unitario = @preco_unitario " +
                "WHERE iditem = @iditem;", conexao);

            cmd.Parameters.AddWithValue("@nomeproduto", itensPedido.NomeProduto);
            cmd.Parameters.AddWithValue("@quantidade", itensPedido.Quantidade);
            cmd.Parameters.AddWithValue("@preco_unitario", itensPedido.PrecoUni);
            cmd.Parameters.AddWithValue("@iditem", itensPedido.IdItem);

            cmd.ExecuteNonQuery();
        }
    }

    public void Excluir(int IdPedido)
    {
        throw new NotImplementedException();
    }
}
using System.Data;
using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
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
                    IdPedido = (int)dr["idpedido"],
                    NomeProduto = (string)dr["nomeproduto"],
                    Quantidade = (int)dr["quantidade"],
                    PrecoUni = (double)dr["preco_unitario"],
                };
                    
                listPedidos.Add(itensPedido);
            }

            return listPedidos;
        }
    }

    public ListaItensPedido ObterTodosPeditos(int IdPedido)
    {
        List<ItensPedido> listaItens = obterItens(IdPedido);
        Pedido pedido = ObterPedido(IdPedido);

        ListaItensPedido list = new ListaItensPedido
        {
            IdPedido = pedido.IdPedido,
            IdUsuario = pedido.IdUsuario,
            ValorTotal = pedido.ValorTotal,
            Pedidos = listaItens
        };
        return list;
    }
    
    public Pedido ObterPedido(int IdPedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM pedido WHERE idpedido = @idpedido;", conexao);

            cmd.Parameters.AddWithValue("@idpedido", IdPedido);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Pedido
                    {
                        IdPedido = (int)reader["idpedido"],
                        IdUsuario = (int)reader["idusuario"], 
                        ValorTotal = (double)reader["valor_total"],
                        Status = (EstadoPedido)reader["sts"]
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
    public List<ItensPedido> obterItens(int IdPedido)
    {
        List<ItensPedido> listPedidos = new List<ItensPedido>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM itens_pedido where idpedido=@idpedido;", conexao);
            cmd.Parameters.Add("@idpedido", MySqlDbType.Int32).Value = IdPedido;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                ItensPedido itensPedido = new ItensPedido
                {
                    IdItem = (int)dr["iditem"],
                    IdUsuario = (int)dr["idusuario"],
                    IdPedido = (int)dr["idpedido"],
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
                        IdPedido = (int)reader["idpedido"],
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
                "INSERT INTO itens_pedido (iditem,idusuario,idpedido, nomeproduto, quantidade, preco_unitario) " +
                "VALUES (default,@idusuario,@idpedido, @nomeproduto, @quantidade, @preco_unitario);", conexao);

            cmd.Parameters.AddWithValue("@idusuario", itensPedido.IdUsuario);
            cmd.Parameters.AddWithValue("@idpedido", itensPedido.IdPedido);
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
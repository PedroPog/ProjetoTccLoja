using System.Data;
using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using LojaCamisa.Repository.Interface.Contract;
using MySql.Data.MySqlClient;

namespace LojaCamisa.Repository.Interface;

public class PedidoRepository : IPedidoRepository
{
    private readonly string _conexao;

    public PedidoRepository(IConfiguration conf)
    {
        _conexao = conf.GetConnectionString("Conexao");
    }
    public IEnumerable<Pedido> ObterTodosPedidosAdm()
    {
        List<Pedido> listPedido = new List<Pedido>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();
            
            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM pedido;", conexao);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                Pedido pedido = new Pedido
                {
                    IdPedido = (int)dr["idpedido"],
                    IdUsuario = (int)dr["idusuario"],
                    ValorTotal = (double)dr["valor_total"],
                    Status = (EstadoPedido)dr["sts"],
                };
                    
                listPedido.Add(pedido);
            }
            
            return listPedido;
        }
    }

    public void FinalizarPedido(int id)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "UPDATE pedido SET sts = 0 WHERE idpedido = @idpedido;", conexao);

            cmd.Parameters.AddWithValue("@idpedido", id);

            cmd.ExecuteReader();
            conexao.Close();
        }
    }
    
    public IEnumerable<Pedido> ObterTodosPedidos(int IdUsuario)
    {
        List<Pedido> listPedido = new List<Pedido>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();
            
            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM pedido WHERE idusuario=@idUsu;", conexao);
            cmd.Parameters.Add("@idUsu", MySqlDbType.Int32).Value = IdUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                Pedido pedido = new Pedido
                {
                    IdPedido = (int)dr["idpedido"],
                    IdUsuario = (int)dr["idusuario"],
                    ValorTotal = (double)dr["valor_total"],
                    Status = (EstadoPedido)dr["sts"],
                };
                    
                listPedido.Add(pedido);
            }
            
            return listPedido;
        }
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

    public void Cadastrar(Pedido pedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO pedido (idpedido,idusuario,valor_total, sts) " +
                "VALUES (default,@idusuario, @valor_total, @sts);", conexao);

            cmd.Parameters.AddWithValue("@idusuario", pedido.IdUsuario);
            cmd.Parameters.AddWithValue("@valor_total", pedido.ValorTotal);
            cmd.Parameters.AddWithValue("@sts", pedido.Status);

            cmd.ExecuteNonQuery();
        }
    }

    public int CadastrarRetorno(Pedido pedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO pedido (idpedido, idusuario, valor_total, sts) " +
                "VALUES (default, @idusuario, @valor_total, @sts);", conexao);

            cmd.Parameters.AddWithValue("@idusuario", pedido.IdUsuario);
            cmd.Parameters.AddWithValue("@valor_total", pedido.ValorTotal);
            cmd.Parameters.AddWithValue("@sts", pedido.Status);

            cmd.ExecuteNonQuery();

            // Obter o ID do pedido recém-criado
            int idPedido = (int)cmd.LastInsertedId;
            return idPedido;
        }
    }

    public void Atualizar(Pedido pedido)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "UPDATE pedido SET valor_total = @valor_total, " +
                "sts = @sts WHERE idpedido = @idpedido;", conexao);

            cmd.Parameters.AddWithValue("@valor_total", pedido.ValorTotal);
            cmd.Parameters.AddWithValue("@sts", pedido.Status);
            cmd.Parameters.AddWithValue("@idpedido", pedido.IdPedido);

            cmd.ExecuteNonQuery();
        }
    }

    public void Excluir(int IdPedido)
    {
        throw new NotImplementedException();
    }
}
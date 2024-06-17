using System.Data;
using LojaCamisa.Models;
using LojaCamisa.Repository.Interface.Contract;
using MySql.Data.MySqlClient;

namespace LojaCamisa.Repository.Interface;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly string _conexao;

    public EnderecoRepository(IConfiguration conf)
    {
        _conexao = conf.GetConnectionString("Conexao");
    }
    
    public IEnumerable<Endereco> ObterTodosEndereco(int IdUsuario)
    {
        List<Endereco> listEndereco = new List<Endereco>();
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();
            
            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM endereco WHERE idusuario=@idUsu;", conexao);
            cmd.Parameters.Add("@idUsu", MySqlDbType.Int32).Value = IdUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                Endereco endereco = new Endereco
                {
                    IdEndereco = (int)dr["idendereco"],
                    IdUsuario = (int)dr["idusuario"],
                    EnderecoCompleto = (string)dr["endereco"],
                    Complemento = (string)dr["complemento"],
                    Cep = (string)dr["cep"],
                    Telefone = (string)dr["telefone"],
                    Responsavel = (bool)dr["responsavel"]
                };
                    
                listEndereco.Add(endereco);
            }
            
            return listEndereco;
        }
         
    }

    public Endereco ObterEndereco(int IdEndereco)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM endereco WHERE idendereco = @idendereco;", conexao);

            cmd.Parameters.AddWithValue("@idendereco", IdEndereco);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Endereco
                    {
                        IdEndereco = (int)reader["idendereco"],
                        IdUsuario = (int)reader["idusuario"],
                        EnderecoCompleto = (string)reader["endereco"],
                        Complemento = (string)reader["complemento"],
                        Cep = (string)reader["cep"],
                        Telefone = (string)reader["telefone"],
                        Responsavel = (bool)reader["responsavel"]
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

    public void Cadastrar(Endereco endereco)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO endereco (idendereco,idusuario, endereco, complemento, cep, telefone,responsavel) " +
                "VALUES (default,@idusuario, @endereco, @complemento, @cep, @telefone,@responsavel);", conexao);

            cmd.Parameters.AddWithValue("@idusuario", endereco.IdUsuario);
            cmd.Parameters.AddWithValue("@endereco", endereco.EnderecoCompleto);
            cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);
            cmd.Parameters.AddWithValue("@cep", endereco.Cep);
            cmd.Parameters.AddWithValue("@telefone", endereco.Telefone);
            cmd.Parameters.AddWithValue("@responsavel", endereco.Responsavel);

            cmd.ExecuteNonQuery();
        }
    }

    public void Atualizar(Endereco endereco)
    {
        using (var conexao = new MySqlConnection(_conexao))
        {
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(
                "UPDATE endereco SET endereco = @endereco, " +
                "complemento = @complemento, cep = @cep, telefone = @telefone, " +
                "responsavel = @responsavel " +
                "WHERE idendereco = @idendereco;", conexao);

            cmd.Parameters.AddWithValue("@endereco", endereco.EnderecoCompleto);
            cmd.Parameters.AddWithValue("@complemento", endereco.Complemento);
            cmd.Parameters.AddWithValue("@cep", endereco.Cep);
            cmd.Parameters.AddWithValue("@telefone", endereco.Telefone);
            cmd.Parameters.AddWithValue("@responsavel", endereco.Responsavel);
            cmd.Parameters.AddWithValue("@idendereco", endereco.IdEndereco);

            cmd.ExecuteNonQuery();
        }
    }

    public void Excluir(int IdEndereco)
    {
        throw new NotImplementedException();
    }
}
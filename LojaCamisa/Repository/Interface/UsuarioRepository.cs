using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using LojaCamisa.Repository.Interface.Contract;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace LojaCamisa.Repository.Interface
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySql;

        public UsuarioRepository(IConfiguration configuration)
        {
            _conexaoMySql = configuration.GetConnectionString("Conexao");
        }

        public Usuario Login(string email, string senha)
        {
            using(var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from usuario where email = @Email and senha = @senha;", conexao);
                cmd.Parameters.Add("@Email",MySqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Senha",MySqlDbType.VarChar).Value = senha;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                MySqlDataReader reader;

                Usuario usuario = new Usuario();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    usuario.idUsuario = (int)reader["idusuario"];
                    usuario.nome = (string)reader["nome"];
                    usuario.email = (string)reader["email"];
                    usuario.tipo = (int)reader["tipo"];
                    usuario.std = (int)reader["std"];
                }
                return usuario;

            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> cliList = new List<Usuario>();
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from usuario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow reader in dt.Rows)
                {
                    cliList.Add(
                        new Usuario
                        {
                            idUsuario = (int)reader["idusuario"],
                            nome = (string)reader["nome"],
                            email = (string)reader["email"],
                            tipo = (int)reader["tipo"],
                            std = (int)reader["std"]
                        });
                }

                return cliList;
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            int Situacao = SituacaoConstant.Ativo;

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into usuario values " +
                    "(default, @nome, @email, @senha, @tipo, @std)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.senha;
                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = usuario.tipo;
                cmd.Parameters.Add("@std", MySqlDbType.VarChar).Value = usuario.std;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE usuario SET nome = @Nome, email = @Email, senha = @Senha WHERE idusuario = @Id", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = usuario.nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = usuario.email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.senha;
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = usuario.idUsuario;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Usuario obterUsuarioId(int id)
        {
            Usuario usuario = null; 
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from usuario where idusuario = @id;", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            idUsuario = reader.GetInt32("idusuario"),
                            nome = reader.GetString("nome"),
                            email = reader.GetString("email"),
                            tipo = reader.GetInt32("tipo"),
                            std = reader.GetInt32("std")
                        };
                    }
                }
            }

            return usuario;
        }


        public void Ativar(int id)
        {
            throw new NotImplementedException();
        }

        public void Desativar(int id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}

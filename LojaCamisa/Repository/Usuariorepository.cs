using LojaCamisa.Models;
using LojaCamisa.Repository.Interface;
using LojaCamisa.Repository.Utils;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaCamisa.Repository
{
    public class Usuariorepository : IUsuarioRepository
    {
        private readonly string _conexao;

        public Usuariorepository(IConfiguration configuration)
        {
            _conexao = configuration.GetConnectionString("Conexao");
        }

        public void Atualizar(Usuario usuario)
        {
            int Situacao = (int)SituacaoEnum.ATIVO;
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET nome=@nome, " +
                    "email=@email, senha=@senha, std=@std WHERE idusuario=@idusuario", conexao);

                cmd.Parameters.Add("@idusuario", MySqlDbType.VarChar).Value = usuario.IdUsuario;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.Senha;
                cmd.Parameters.Add("@std", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void AtualizarAdm(Usuario usuario)
        {
            //int Situacao = (int)SituacaoEnum.ATIVO;
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE usuario SET nome=@nome, " +
                    "email=@email, senha=@senha, tipo=@tipo, std=@std WHERE idusuario=@idusuario", conexao);

                cmd.Parameters.Add("@idusuario", MySqlDbType.VarChar).Value = usuario.IdUsuario;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.Senha;
                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = usuario.Tipo;
                cmd.Parameters.Add("@std", MySqlDbType.VarChar).Value = usuario.Status;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO usuario( nome,email,senha) " +
                    "values (@nome,@email,@senha);", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.Senha;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void CadastrarAdm(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO usuario( nome,email,senha,tipo) " +
                    "values (@nome,@email,@senha,@tipo);", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.Senha;
                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = usuario.Tipo;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM usuario WHERE idusuario=@idusuario", conexao);

                cmd.Parameters.AddWithValue("@idusuario", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Usuario Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE email = @email AND senha = @senha ;", conexao);

                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                MySqlDataReader reader;

                Usuario usuario = new Usuario();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    usuario.IdUsuario = (int)reader["idusuario"];
                    usuario.Nome = (string)reader["nome"];
                    usuario.Email = (string)reader["email"];
                    usuario.Tipo = (TipoEnum)reader["tipo"];
                    usuario.Status = (SituacaoEnum)reader["std"];
                }
                conexao.Close();
                return usuario;
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario",conexao);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                conexao.Close();
                foreach(DataRow row in dataTable.Rows)
                {
                    usuarios.Add(
                        new Usuario
                        {
                            IdUsuario = (int)row["idusuario"],
                            Nome = (string)row["nome"],
                            Email = (string)row["email"],
                            Tipo = (TipoEnum)row["tipo"]
                        });
                }
                return usuarios;
            }
        }

        public Usuario ObterUsuario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE idusuario=@idusuario", conexao);
                cmd.Parameters.AddWithValue("@idusuario", Id);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                MySqlDataReader reader;

                Usuario usuario = new Usuario();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    usuario.IdUsuario = (int)reader["idusuario"];
                    usuario.Nome = (string)reader["nome"];
                    usuario.Email = (string)reader["email"];
                    usuario.Senha = (string)reader["senha"];
                    usuario.Tipo = (TipoEnum)reader["tipo"];
                    usuario.Status = (SituacaoEnum)reader["std"];
                }
                return usuario;
            }
        }
    }
}

﻿using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using LojaCamisa.Repository.Interface.Contract;
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

                MySqlCommand cmd = new MySqlCommand("select * from Usuario where email = @Email and senha = @senha;", conexao);
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

                MySqlCommand cmd = new MySqlCommand("select * from Usuario", conexao);

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
                    "(@Nome, @email, @senha, @tipo, @std)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = usuario.nome;
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
            throw new NotImplementedException();
        }

        public Usuario obterUsuarioId(int id)
        {
            throw new NotImplementedException();
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
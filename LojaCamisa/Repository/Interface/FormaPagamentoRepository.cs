using System.Data;
using LojaCamisa.Models;
using LojaCamisa.Repository.Interface.Contract;
using MySql.Data.MySqlClient;

namespace LojaCamisa.Repository.Interface
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly string _conexao;

        public FormaPagamentoRepository(IConfiguration conf)
        {
            _conexao = conf.GetConnectionString("Conexao");
        }

        public IEnumerable<FormaPagamento> ObterTodosPag(int id)
        {
            List<FormaPagamento> listFormas = new List<FormaPagamento>();
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM formapagamento WHERE idusuario=@idUsu;", conexao);
                cmd.Parameters.Add("@idUsu", MySqlDbType.Int32).Value = id;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    FormaPagamento pagamento = new FormaPagamento
                    {
                        IdFormaPagamento = (int)dr["idformapagamento"],
                        NomeCompleto = (string)dr["nomecompleto"],
                        Numero = (string)dr["numero"],
                        Vencimento = (string)dr["vencimento"],
                        CVV = (string)dr["cvv"],
                        IdUsuario = (int)dr["idusuario"]
                    };
                    
                    listFormas.Add(pagamento);
                }

                return listFormas;
            }
        }

        public void Cadastrar(FormaPagamento formaPagamento)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO formapagamento (idformapagamento,nomecompleto, numero, vencimento, cvv, idusuario) " +
                    "VALUES (default,@nomeCompleto, @numero, @vencimento, @cvv, @idUsuario);", conexao);

                cmd.Parameters.AddWithValue("@nomeCompleto", formaPagamento.NomeCompleto);
                cmd.Parameters.AddWithValue("@numero", formaPagamento.Numero);
                cmd.Parameters.AddWithValue("@vencimento", formaPagamento.Vencimento);
                cmd.Parameters.AddWithValue("@cvv", formaPagamento.CVV);
                cmd.Parameters.AddWithValue("@idUsuario", formaPagamento.IdUsuario);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualziar(FormaPagamento formaPagamento)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE formapagamento SET nomecompleto = @nomeCompleto, " +
                    "numero = @numero, vencimento = @vencimento, cvv = @cvv " +
                    "WHERE idformapagamento = @idformapagamento;", conexao);

                cmd.Parameters.AddWithValue("@nomeCompleto", formaPagamento.NomeCompleto);
                cmd.Parameters.AddWithValue("@numero", formaPagamento.Numero);
                cmd.Parameters.AddWithValue("@vencimento", formaPagamento.Vencimento);
                cmd.Parameters.AddWithValue("@cvv", formaPagamento.CVV);
                cmd.Parameters.AddWithValue("@idformapagamento", formaPagamento.IdFormaPagamento);

                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int IdFormaPagamento)
        {
            throw new NotImplementedException();
        }

        public FormaPagamento ObterFormaPag(int IdFormaPagamento)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM formapagamento WHERE idformapagamento = @idformapagamento;", conexao);

                cmd.Parameters.AddWithValue("@idformapagamento", IdFormaPagamento);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new FormaPagamento
                        {
                            IdFormaPagamento = (int)reader["idformapagamento"],
                            NomeCompleto = (string)reader["nomecompleto"],
                            Numero = (string)reader["numero"],
                            Vencimento = (string)reader["vencimento"],
                            CVV = (string)reader["cvv"],
                            IdUsuario = (int)reader["idusuario"]
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
    }
}
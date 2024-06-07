using LojaCamisa.Models;
using MySqlX.XDevAPI;
using Newtonsoft.Json;

namespace LojaCamisa.Libraries.Login
{
    public class LoginUsuario
    {
        private string Key = "Login";
        private Sessao.Sessao _sessao;
        public LoginUsuario(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Usuario usuario)
        {
            string clienteJSONString = JsonConvert.SerializeObject(usuario);

            _sessao.Cadastrar(Key, clienteJSONString);
        }

        public Usuario GetCliente()
        {
            if (_sessao.Existe(Key))
            {
                string clienteJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Usuario>(clienteJSONString);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}

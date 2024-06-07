using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract
{
    public interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);

        void Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Excluir(int id);
        Usuario obterUsuarioId(int id);
        IEnumerable<Usuario> ObterTodosUsuarios();
        void Ativar(int id);
        void Desativar(int id);
    }
}

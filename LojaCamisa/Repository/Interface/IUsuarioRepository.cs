using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);

        void Cadastrar(Usuario usuario);
        void CadastrarAdm(Usuario usuario);
        void Atualizar(Usuario usuario);
        void AtualizarAdm(Usuario usuario);
        void Excluir(int Id);
        Usuario ObterUsuario(int Id);
        IEnumerable<Usuario> ObterTodosUsuario();
        //IPageList<Usuario> ObterTodosUsuarios(int? pagina, string pesquisa);
    }
}

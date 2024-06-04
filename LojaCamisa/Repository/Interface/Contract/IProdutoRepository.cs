using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract
{
    public interface IProdutoRepository
    {
        IEnumerable<Produtos> ObterTodosProdutos();
        Produtos ObterProduto(int id);
        void CadastrarProduto(Produtos produtos);
        void Atualizar(Produtos produtos);
        void AtualizarEstoque(int Quantidade);
        void Excluir(int id);
    }
}

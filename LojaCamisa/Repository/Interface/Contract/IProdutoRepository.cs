using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract
{
    public interface IProdutoRepository
    {
        IEnumerable<Produtos> ObterTodosProdutos();
        Produtos ObterProduto(int id);
        int CadastrarProduto(ProdutoTemp produtos);
        void CadastrarImgs(IFormFile file, int id);
        void Atualizar(Produtos produtos);
        void AtualizarEstoque(int Quantidade);
        void Excluir(int id);
    }
}

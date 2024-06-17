using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract
{
    public interface IProdutoRepository
    {
        IEnumerable<Produtos> ObterTodosProdutos();
        Produtos ObterProduto(int id);
        void CadastrarProduto(ProdutoTemp produtos,List<IFormFile> file);
        void Atualizar(Produtos produtos);
        void AtualizarEstoque(int Quantidade);
        void Excluir(int id);
    }
}

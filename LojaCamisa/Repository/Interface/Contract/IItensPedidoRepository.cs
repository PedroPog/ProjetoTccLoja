using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract;

public interface IItensPedidoRepository
{
    IEnumerable<ItensPedido> ObterTodosPeditosAdm();
    ListaItensPedido ObterTodosPeditos(int IdPedido);
    ItensPedido ObterItemPedido(int IdItemPedido);
    void Cadastrar(ItensPedido itensPedido);
    void Atualizar(ItensPedido itensPedido);
    void Excluir(int IdPedido);
}
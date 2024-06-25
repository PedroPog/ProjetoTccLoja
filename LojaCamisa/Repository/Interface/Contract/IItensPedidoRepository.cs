using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract;

public interface IItensPedidoRepository
{
    IEnumerable<ItensPedido> ObterTodosPeditosAdm();
    ListaItensPedido ObterTodosPeditos(int IdPedido);
    ItensPedido ObterItemPedido(int IdItemPedido);
    IEnumerable<ItensPedido> ObterItemPedidos(int id);
    void Cadastrar(ItensPedido itensPedido);
    void Atualizar(ItensPedido itensPedido);
    void Excluir(int IdPedido);
    double ListarTotal(int id);
}
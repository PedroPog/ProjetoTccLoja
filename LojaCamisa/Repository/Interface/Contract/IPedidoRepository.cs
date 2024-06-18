using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract;

public interface IPedidoRepository
{
    IEnumerable<Pedido> ObterTodosPedidosAdm();
    IEnumerable<Pedido> ObterTodosPedidos(int IdUsuario);
    Pedido ObterPedido(int IdPedido);
    void Cadastrar(Pedido pedido);
    void Atualizar(Pedido pedido);
    void Excluir(int IdPedido);
}
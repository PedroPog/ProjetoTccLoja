using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract;

public interface IPedidoRepository
{
    IEnumerable<Pedido> ObterTodosPedidosAdm();
    IEnumerable<Pedido> ObterTodosPedidos(int IdUsuario);
    Pedido ObterPedido(int IdPedido);
    void Cadastrar(Pedido pedido);
    int CadastrarRetorno(Pedido pedido);
    void Atualizar(Pedido pedido);
    void FinalizarPedido(int id);
    void Excluir(int IdPedido);
}
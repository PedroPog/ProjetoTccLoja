namespace LojaCamisa.Models;

public class PedidoViewModel
{
    public Pedido Pedido { get; set; }
    public List<ItensPedido> ItensPedidos { get; set; }
}
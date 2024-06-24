using LojaCamisa.Models.Constant;

namespace LojaCamisa.Models;

public class ListaItensPedido
{
    public int IdUsuario { get; set; }
    public int IdPedido { get; set; }
    public EstadoPedido Status { get; set; }
    public List<ItensPedido> Pedidos { get; set; }
    public double ValorTotal { get; set; }
}
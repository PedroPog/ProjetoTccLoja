using LojaCamisa.Models.Constant;

namespace LojaCamisa.Models;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdUsuario { get; set; }
    public double ValorTotal { get; set; }
    public EstadoPedido Status { get; set; }
}
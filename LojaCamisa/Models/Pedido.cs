namespace LojaCamisa.Models;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdUsuario { get; set; }
    public double ValorTotal { get; set; }
    public int Status { get; set; }
}
namespace LojaCamisa.Models;

public class ListaItensPedido
{
    public int IdUsuario { get; set; }
    public int IdPedido { get; set; }
    public List<ItensPedido> Pedidos { get; set; }
    public double ValorTotal { get; set; }
}
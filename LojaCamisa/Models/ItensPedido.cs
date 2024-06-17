namespace LojaCamisa.Models;

public class ItensPedido
{
    public int IdItem { get; set; }
    public int IdUsuario { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public double PrecoUni { get; set; }
}
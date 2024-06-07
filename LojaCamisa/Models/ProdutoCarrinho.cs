namespace LojaCamisa.Models
{
    public class ProdutoCarrinho
    {
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
        public string imagem { get; set; }
    }
}

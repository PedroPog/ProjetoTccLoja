namespace LojaCamisa.Models
{
    public class ProdutoTemp
    {
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public string descricao { get; set; }
        public string lancamento { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
        public bool status { get; set; }
        public string marca { get; set; }
        public bool nacional { get; set; }
        public string modalidade { get; set; }
        public List<string> imagem { get; set; }
    }
}

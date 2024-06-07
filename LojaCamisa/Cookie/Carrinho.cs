using LojaCamisa.Models;
using Newtonsoft.Json;
using System.Drawing;

namespace LojaCamisa.Cookie
{
    public class Carrinho
    {
        private string Key = "COOKIE_DEFAULT";
        private Cookie _cookie;
        private readonly IHttpContextAccessor _contextAccessor;

        public Carrinho(Cookie cookie, IHttpContextAccessor contextAccessor)
        {
            _cookie = cookie;
            _contextAccessor = contextAccessor;
        }

        public void Salvar(List<ProdutoCarrinho> list)
        {
            string Valor = JsonConvert.SerializeObject(list);
            _cookie.CadastrarCookie(Key, Valor);
            
        }

        public List<ProdutoCarrinho> Consultar()
        {

            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<ProdutoCarrinho>>(valor);
            }
            else
            {
                return new List<ProdutoCarrinho>();
            }
        }

        public void Cadastrar(ProdutoCarrinho item)
        {
            List<ProdutoCarrinho> lista;
            if (_cookie.Existe(Key))
            {
                lista = Consultar();
                var itemLocalizado = lista.SingleOrDefault(a => a.idProduto == item.idProduto);

                if (itemLocalizado != null)
                {
                    itemLocalizado.quantidade += item.quantidade; // Adiciona a quantidade do novo item
                }
                else
                {
                    lista.Add(item);
                }
            }
            else
            {
                lista = new List<ProdutoCarrinho> { item };
            }
            Salvar(lista);
        }
        public void Atualizar(ProdutoCarrinho item)
        {
                var Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.idProduto == item.idProduto);
                if (ItemLocalizado != null)
                {
                    ItemLocalizado.quantidade = item.quantidade + 1;
                    Salvar(Lista);
                }
        }
        public void Remover(ProdutoCarrinho item)
        {
                var Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.idProduto == item.idProduto);


                if (ItemLocalizado != null)
                {
                    Lista.Remove(ItemLocalizado);
                    Salvar(Lista);
                }
        }
        public bool Existe(string key)
        {
            return _cookie.Existe(key);
        }

        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}

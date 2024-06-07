using LojaCamisa.Cookie;
using LojaCamisa.Libraries.Filtro;
using LojaCamisa.Libraries.Login;
using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using LojaCamisa.Repository.Interface.Contract;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Diagnostics;

namespace LojaCamisa.Controllers
{
    public class HomeController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private IUsuarioRepository _usuarioRepository;
        private LoginUsuario _loginUsuario;
        private Carrinho _carrinho;

        public HomeController(IProdutoRepository produtoRepository,Carrinho carrinho,
            LoginUsuario loginUsuario, IUsuarioRepository usuarioRepository)
        {
            _produtoRepository = produtoRepository;
            _carrinho = carrinho;
            _loginUsuario = loginUsuario;
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }
        public IActionResult DetalhesProdutos(int id)
        {
            return View(_produtoRepository.ObterProduto(id));
        }
        public IActionResult Carrinho()
        {
            var carrinho = _carrinho.Consultar();
            return View(carrinho);
        }
        public IActionResult AdicionarItem(int id)
        {
            Produtos produtos = _produtoRepository.ObterProduto(id);
            if(produtos == null)
            {
                return View("Corrigir");
            }
            else
            {
                var item = new ProdutoCarrinho()
                {
                    idProduto = id,
                    quantidade = 1,
                    imagem = produtos.imagem[0],
                    preco = produtos.preco,
                    nomeProduto = produtos.nomeProduto
                };
                _carrinho.Cadastrar(item);;
                return RedirectToAction("Carrinho");
            }
        }

        public IActionResult RemoverProduto(int id)
        {
            _carrinho.Remover(new ProdutoCarrinho() { idProduto = id });
            return RedirectToAction(nameof(Carrinho));
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Usuario usuario)
        {
            usuario.std = SituacaoConstant.Ativo;
            _usuarioRepository.Cadastrar(usuario);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel usuario)
        {
            Usuario cleinteDB = _usuarioRepository.Login(usuario.Email, usuario.Senha);

            if (cleinteDB.email != null && usuario.Senha != null)
            {
                _loginUsuario.Login(cleinteDB);
                return new RedirectResult(Url.Action(nameof(Index)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }
        }

        [ClienteAuth]
        public IActionResult LogoutCliente()
        {
            _loginUsuario.Logout();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult FiltrarPorMarca(string marcaSelecionada)
        {
            return View("index");
        }

    }
}

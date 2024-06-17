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

        public IActionResult PerfilCliente(int id)
        {
            var usuario = _usuarioRepository.obterUsuarioId(id);
            return View(usuario);
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
            usuario.tipo = 0;
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


        // Método para retornar a view parcial do perfil
        public IActionResult Perfil()
        {
            var usuario = _loginUsuario.GetCliente(); // Obtém o usuário logado, ajuste conforme necessário
            return PartialView("_Perfil", usuario);
        }

        public IActionResult EditarPerfil(int id)
        {
            var usu = _usuarioRepository.obterUsuarioId(id);
            return PartialView("_EditarPerfil", usu);
        }

        [HttpPost]
        public IActionResult AtualizarPerfil(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Atualizar(usuario);
                return RedirectToAction(nameof(PerfilCliente), new { id = usuario.idUsuario });
            }

            // Se houver erros de validação, retorne para a view de edição com os dados do usuário
            return PartialView("_EditarPerfil", usuario);
        }

        // Método para retornar a view parcial de pedidos
        public IActionResult Pedidos()
        {
            return PartialView("_Pedidos");
        }

        // Método para retornar a view parcial de endereço
        public IActionResult Endereco()
        {
            return PartialView("_Endereco");
        }

        // Método para retornar a view parcial de pagamentos
        public IActionResult Pagamentos()
        {
            return PartialView("_Pagamentos");
        }
    }
}

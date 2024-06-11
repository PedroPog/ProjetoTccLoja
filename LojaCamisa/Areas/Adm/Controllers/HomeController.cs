using LojaCamisa.GerenciadorArquivos;
using LojaCamisa.Libraries.Filtro;
using LojaCamisa.Libraries.Login;
using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using LojaCamisa.Repository.Interface.Contract;
using Microsoft.AspNetCore.Mvc;

namespace LojaCamisa.Areas.Adm.Controllers
{
    [Area("Adm")]
    [ClienteAuth(UsuarioTipoConstant.Gerente)]
    public class HomeController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private IProdutoRepository _produtoRepository;
        private LoginUsuario _loginUsuario;

        public HomeController(IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProdutoList()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        public IActionResult Pedidos()
        {
            return View();
        }

        public IActionResult Person()
        {
            return View();
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produtos produtos, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        GerenciadorArquivo.CadastrarImagemProduto(file);
                    }
                }

                _produtoRepository.CadastrarProduto(produtos);
                TempData["MSG_S"] = "Produto cadastrado com sucesso!";
                return RedirectToAction(nameof(ProdutoList));
            }

            return View(produtos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Models.Usuario usuario)
        {
            usuario.tipo = UsuarioTipoConstant.Comum;
            _usuarioRepository.Cadastrar(usuario);
            TempData["MSG_S"] = "Registro salvo com sucesso!";
            return RedirectToAction(nameof(ProdutoList));
        }

        [ClienteAuth]
        public IActionResult Painel()
        {
            return View();
        }
        [ClienteAuth]
        public IActionResult Logout()
        {
            _loginUsuario.Logout();
            return RedirectToAction("Login", "Home");
        }
    }
}

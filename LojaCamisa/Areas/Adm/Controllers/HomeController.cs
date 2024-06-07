using LojaCamisa.Libraries.Filtro;
using LojaCamisa.Libraries.Login;
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

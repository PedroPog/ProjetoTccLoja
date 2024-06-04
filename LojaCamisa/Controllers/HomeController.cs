using LojaCamisa.Models;
using LojaCamisa.Repository.Interface.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaCamisa.Controllers
{
    public class HomeController : Controller
    {
        private IProdutoRepository _produtoRepository;

        public HomeController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }
        public IActionResult DetalhesProdutos(int id)
        {
            return View(_produtoRepository.ObterProduto(id));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FiltrarPorMarca(string marcaSelecionada)
        {
            return View("index");
        }

    }
}

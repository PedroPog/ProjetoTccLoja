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
        private IEnderecoRepository _enderecoRepository;
        private IProdutoRepository _produtoRepository;
        private IUsuarioRepository _usuarioRepository;
        private IFormaPagamentoRepository _pagamentoRepository;
        private IItensPedidoRepository _itemPedidoRepository;
        private IPedidoRepository _pedidoRepository;
        private LoginUsuario _loginUsuario;
        private Carrinho _carrinho;

        public HomeController(IProdutoRepository produtoRepository,Carrinho carrinho,
            LoginUsuario loginUsuario, IUsuarioRepository usuarioRepository,
            IFormaPagamentoRepository pagamentoRepository,IItensPedidoRepository itemPedidoRepository,
            IPedidoRepository pedidoRepository, IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _produtoRepository = produtoRepository;
            _carrinho = carrinho;
            _loginUsuario = loginUsuario;
            _usuarioRepository = usuarioRepository;
            _pagamentoRepository = pagamentoRepository;
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
                ViewData["MSG_E"] = "Usu�rio n�o localizado, por favor verifique e-mail e senha digitado";
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


        // M�todo para retornar a view parcial do perfil
        public IActionResult Perfil()
        {
            var usuario = _loginUsuario.GetCliente(); // Obt�m o usu�rio logado, ajuste conforme necess�rio
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

            // Se houver erros de valida��o, retorne para a view de edi��o com os dados do usu�rio
            return PartialView("_EditarPerfil", usuario);
        }

        // M�todo para retornar a view parcial de pedidos
        public IActionResult Pedidos()
        {
            var list = _pedidoRepository.ObterTodosPedidos(_loginUsuario.GetCliente().idUsuario);
            return PartialView("_Pedidos",list);
        }

        public IActionResult DetalhesPedido(int id)
        {
            var list = _itemPedidoRepository.ObterTodosPeditos(id);
            return PartialView("_DetalhesPedido", list);
        }
        
        public IActionResult FinalizarPedido()
        {
            //_pedidoRepository.FinalizarPedido();
            List<ProdutoCarrinho> carrinho = _carrinho.Consultar();
            
            Pedido pedido = new Pedido()
            {
                IdUsuario = _loginUsuario.GetCliente().idUsuario,
                ValorTotal = 0.0,
                Status = EstadoPedido.ANALISE
            };
            int idPedido = _pedidoRepository.CadastrarRetorno(pedido);
            foreach (var produtoCarrinho in carrinho)
            {
                ItensPedido pedidos = new ItensPedido()
                {
                    IdPedido = idPedido,
                    IdUsuario = _loginUsuario.GetCliente().idUsuario,
                    IdProduto = produtoCarrinho.idProduto,
                    Quantidade = produtoCarrinho.quantidade,
                    NomeProduto = produtoCarrinho.nomeProduto,
                    PrecoUni = produtoCarrinho.preco
                };
                _itemPedidoRepository.Cadastrar(pedidos);
            }

            double valorTotal = _itemPedidoRepository.ListarTotal(idPedido);
            pedido = new Pedido()
            {
                IdPedido = idPedido,
                ValorTotal = valorTotal,
                Status = EstadoPedido.ACEITO
            };
            _pedidoRepository.Atualizar(pedido);
            _carrinho.RemoverTodos();
            return RedirectToAction("Index");
        }
        
        public IActionResult EditarPedido(int id)
        {
            var usu = _itemPedidoRepository.ObterItemPedido(id);
            return PartialView("_EditarPedido", usu);
        }

        // M�todo para retornar a view parcial de endere�o
        public IActionResult Endereco()
        {
            var list = _enderecoRepository.ObterTodosEndereco(_loginUsuario.GetCliente().idUsuario);
            return PartialView("_Endereco",list);
        }
        public IActionResult EditarEndereco(int id)
        {
            Endereco forma = _enderecoRepository.ObterEndereco(id);
            return PartialView("_EditarEndereco", forma);
        }
        [HttpPost]
        public IActionResult AtualizarEndereco(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                _enderecoRepository.Atualizar(endereco);
                return RedirectToAction(nameof(PerfilCliente), new { id = endereco.IdUsuario });
            }

            // Se houver erros de valida��o, retorne para a view de edi��o com os dados do usu�rio
            return PartialView("_EditarEndereco", endereco);
        }
        public IActionResult CadastrarEndereco()
        {
            return PartialView("_CadastrarEndereco");
        }
        
        [HttpPost]
        public IActionResult CadastrarEndereco(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _enderecoRepository.Cadastrar(endereco);
                    return RedirectToAction(nameof(PerfilCliente), new { id = _loginUsuario.GetCliente().idUsuario });
                }
                catch (Exception ex)
                {
                    return PartialView("_CadastrarEndereco",endereco);
                }
            }
            return PartialView("_CadastrarEndereco",endereco);
        }

        // M�todo para retornar a view parcial de pagamentos
        public IActionResult Pagamentos()
        {
            int id = _loginUsuario.GetCliente().idUsuario;
            var forma = _pagamentoRepository.ObterTodosPag(id);
            return PartialView("_Pagamentos",forma);
        }

        public IActionResult CadastrarPagamento()
        {
            return PartialView("_CadastrarPagamento");
        }
        
        [HttpPost]
        public IActionResult CadastrarPagamento(FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pagamentoRepository.Cadastrar(formaPagamento);
                    return RedirectToAction(nameof(PerfilCliente), new { id = _loginUsuario.GetCliente().idUsuario });
                }
                catch (Exception ex)
                {
                    return PartialView("_CadastrarPagamento",formaPagamento);
                }
            }
            return PartialView("_CadastrarPagamento",formaPagamento);
        }
        public IActionResult EditarPagamentos(int id)
        {
            FormaPagamento forma = _pagamentoRepository.ObterFormaPag(id);
            return PartialView("_EditarPagamentos", forma);
        }

        [HttpPost]
        public IActionResult AtualizarPagamentos(FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                _pagamentoRepository.Atualziar(formaPagamento);
                return RedirectToAction(nameof(PerfilCliente), new { id = formaPagamento.IdUsuario });
            }

            // Se houver erros de valida��o, retorne para a view de edi��o com os dados do usu�rio
            var usuario = _loginUsuario.GetCliente();
            return PartialView("_EditarPerfil", usuario);
        }
    }
}

using LojaCamisa.Libraries.Login;
using LojaCamisa.Models;
using LojaCamisa.Models.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaCamisa.Libraries.Filtro
{
    public class ClienteAuthAttribute : Attribute, IAuthorizationFilter
    {
        LoginUsuario _loginUsuario;
        private int _tipoAuth;
        public ClienteAuthAttribute(int tipoAuth = UsuarioTipoConstant.Comum)
        {
            _tipoAuth = tipoAuth;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));
            Usuario usuario = _loginUsuario.GetCliente();
            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (usuario.tipo == UsuarioTipoConstant.Comum && _tipoAuth == UsuarioTipoConstant.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}

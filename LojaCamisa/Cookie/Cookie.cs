using Microsoft.EntityFrameworkCore;

namespace LojaCamisa.Cookie
{
    public class Cookie
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        
        public Cookie(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public void CadastrarCookie(string key, string value)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            };
            _contextAccessor.HttpContext.Response.Cookies.Append(key,value,options);
        }
        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                Remover(key);
            }
            CadastrarCookie(key, valor);
        }

        public void Remover(string key)
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public string Consultar(string key, bool Cript = true)
        {
            var valor = _contextAccessor.HttpContext.Request.Cookies[key];
            return valor;
        }

        public bool Existe(string key)
        {
            bool existe = _contextAccessor.HttpContext.Request.Cookies[key] != null;
            return existe;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaCamisa.Libraries.Filtro
{
    public class ClienteAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}

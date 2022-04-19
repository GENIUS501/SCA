using System.Web;
using System.Web.Mvc;

namespace SCA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //Este filtro verifica la existencia de la sesion de usuario si la sesione existe permite el ingreso al sistema de lo contrario redirige al login
            filters.Add(new Filters.Verificarsession());
        }
    }
}

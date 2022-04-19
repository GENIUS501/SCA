using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters
{
    //No permite mulitples
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUserPermises : AuthorizeAttribute
    {
        //Declaracion de variables

        private int numero_modulo;
        private string accion;
        //Captura el numero del modulo al que se desea acceder 
        public AuthorizeUserPermises(string accion, int idmodulo = 0)
        {
            this.numero_modulo = idmodulo;
            this.accion = accion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {
                //Le asigna el valor de la sesion al objeto de tipo entidad usuario
                var Usuario = (EUsuario)HttpContext.Current.Session["User"];
                if (Usuario == null)
                {
                    //Envia el error a pantalla
                    filterContext.Result = new RedirectResult("~/Acceso/Login");
                }
                else
                {
                    //Valida que la accion este permitida en el perfil.
                    //Llena la entidad permisos con los valores de la tabla permisos de base de datos si existen
                    NRoles_Permisos Negocios = new NRoles_Permisos();
                    var lstMisOperaciones = Negocios.ListaOperaciones(Usuario.IdRol, numero_modulo, accion);
                    //Si es meno o igual a cero es que el permiso no existe y por lo tanto no puede acceder al modulo
                    if (lstMisOperaciones.ToList().Count() <= 0)
                    {
                        //Envia el error a pantalla
                        filterContext.Result = new RedirectResult("~/Home/Error");
                    }
                }
            }
            catch (Exception)
            {
                //Envia el error a pantalla
                filterContext.Result = new RedirectResult("~/Home/Error");
            }
        }
    }
}
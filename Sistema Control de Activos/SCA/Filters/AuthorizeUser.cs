using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace Filters
{
    //No permite mulitples
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        //Declaracion de variables
        private string numero_modulo;
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        //Captura el numero del modulo al que se desea acceder 
        public AuthorizeUser(string idmodulo)
        {
            this.numero_modulo = idmodulo;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                //Le asigna el valor de la sesion al objeto de tipo entidad usuario
                var UsuarioEntidadSesion = (Usuario)HttpContext.Current.Session["User"];
                if (UsuarioEntidadSesion == null)
                {
                    //Envia el error a pantalla
                    filterContext.Result = new RedirectResult("~/Acceso/Login");
                }
                else
                {
                    //Llena la entidad permisos con los valores de la tabla permisos de base de datos si existen
                    int IdPerfil = (int)((UsuarioEntidadSesion.IdPerfiles is null )? UsuarioEntidadSesion.IdPerfiles:0);
                    var lstMisOperaciones = Lista_de_Operaciones(IdPerfil, numero_modulo);
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
        public List<Perfiles_Permisos> Lista_de_Operaciones(int Idrol, string IdModulo)
        {
            try
            {
                using (db)
                {
                    List<Perfiles_Permisos> Objbd = new List<Perfiles_Permisos>();
                    return db.Perfiles_Permisos.Where(x => x.Id_Permiso == Idrol && x.Modulo == IdModulo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
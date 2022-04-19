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
    public class AuthorizeUserPermises : AuthorizeAttribute
    {
        //Declaracion de variables
        private string numero_modulo;
        private string accion;
        //Captura el numero del modulo al que se desea acceder 
        public AuthorizeUserPermises(string accion, string idmodulo)
        {
            this.numero_modulo = idmodulo;
            this.accion = accion;
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
                    //Valida que la accion este permitida en el perfil.
                    //Llena la entidad permisos con los valores de la tabla permisos de base de datos si existen
                    //NRoles_Permisos Negocios = new NRoles_Permisos();
                    int IdPerfil = (int)(UsuarioEntidadSesion.IdPerfiles);
                    var lstMisOperaciones = Lista_de_Operaciones_Accion(IdPerfil, numero_modulo, accion);
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
        public List<Perfiles_Permisos> Lista_de_Operaciones_Accion(int IdPerfil, string IdModulo, string Accion)
        {
            try
            {
                using (BaseDatosSCAEntities db = new BaseDatosSCAEntities())
                {
                    List<Perfiles_Permisos> Objbd = new List<Perfiles_Permisos>();
                    if (Accion == "A")
                    {
                        Objbd = db.Perfiles_Permisos.Where(x => x.Id_Perfil == IdPerfil && x.Modulo == IdModulo && x.Agregar == "S").ToList();
                    }
                    if (Accion == "E")
                    {
                        Objbd = db.Perfiles_Permisos.Where(x => x.Id_Perfil == IdPerfil && x.Modulo == IdModulo && x.Modificar == "S").ToList();
                    }
                    if (Accion == "D")
                    {
                        Objbd = db.Perfiles_Permisos.Where(x => x.Id_Perfil == IdPerfil && x.Modulo == IdModulo && x.Eliminar == "S").ToList();
                    }
                    return Objbd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
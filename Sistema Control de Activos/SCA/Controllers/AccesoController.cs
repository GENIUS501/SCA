using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class AccesoController : Controller
    {
        BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //Recibe el usuario y la contrasena del sistema
        public ActionResult Login(LoginViewModel Modelo)
        {
            try
            {
                //Encripta la contrasena para realizar la prueba
                string passa = Helpers.Helper.EncodePassword(string.Concat(Modelo.Usuario.ToString(), Modelo.Pass.ToString()));
                //Se consulta la base de datos con la contrasena y el usuario dados por usuario 
                var UsuarioLogueado = Login(Modelo.Usuario, passa);
                //Si la base no devolvio la entidad usuarios llena es por que el usuario o la clave esta mal
                if (UsuarioLogueado == null)
                {
                    //Expresa el error a la vista
                    //ViewBag.Error = "Usuario o Contrasena invalida!!!";
                    //Notifica que la clave esta incorrecta
                    TempData["msg"] = "<script>alert('Usuario o contraseña incorrectos!!!');</script>";
                    return View();
                }
                else
                {
                    ////Crea la sesion con la que el sistema validara
                    BitacoraIngresoSalida Entidad = new BitacoraIngresoSalida();
                    Entidad.IdUsuario = UsuarioLogueado.IdUsuario;
                    Entidad.FechaIngreso = DateTime.Now;
                    int id_sesion = Helpers.Helper.RegistrarIngresoSalida(Entidad);
                    //if (id_sesion == 0)
                    //{
                    //    TempData["msg"] = "<script>alert('Error al ingresar al sistema!!!');</script>";

                    //    return View();
                    //}
                    Session["id_sesion"] = id_sesion;
                    Session["User"] = UsuarioLogueado;
                    Session["Usuario"] = UsuarioLogueado.Usuario1;
                    TempData["msg"] = "<script>alert('Bienvenido " + UsuarioLogueado.Usuario1 + "');</script>";
                    //Redirige a la pagina principal del sistema
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Login", "Acceso");
                //   return View();
            }
        }

        public Usuario Login(string Usuario, string Pass)
        {
            try
            {
                Usuario Objbd = new Usuario();
                using (db)
                {
                    Objbd = db.Usuario.Where(x => x.Usuario1 == Usuario && x.Contraseña == Pass).FirstOrDefault();
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
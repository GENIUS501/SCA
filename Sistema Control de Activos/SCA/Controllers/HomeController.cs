using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public void Salir()
        {
            int sesion = int.Parse(Session["id_sesion"].ToString());
            int resultado = Helpers.Helper.RegistroSalida(sesion);
            Session["User"] = null;
            Session["Usuario"] = null;
            Session["id_sesion"] = null;
            if (resultado > 0)
            {
                Response.Redirect("~/Acceso/Login");
            }
        }
    }
}
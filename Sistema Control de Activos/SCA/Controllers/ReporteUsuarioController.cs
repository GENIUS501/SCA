using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteUsuarioController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteUsuario = new List<Usuario>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteUsuario = dc.Usuario.Where(a => a.IdUsuario != null).ToList();
            }
            return View(ReporteUsuario);
        }
    }
}
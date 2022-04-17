using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteControlInventarioController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteControlInventario = new List<ControlInventario>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteControlInventario = dc.ControlInventario.Where(a => a.IdControlInventario != null).ToList();
            }
            return View(ReporteControlInventario);
        }
    }
}
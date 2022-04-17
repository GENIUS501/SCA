using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteInventarioController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteInventario = new List<Inventario>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteInventario = dc.Inventario.Where(a => a.IdInventario != null).ToList();
            }
            return View(ReporteInventario);
        }
    }
}
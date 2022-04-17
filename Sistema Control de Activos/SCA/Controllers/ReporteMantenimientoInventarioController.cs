using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteMantenimientoInventarioController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteMantenimientoInventario = new List<MantenimientoInventario>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteMantenimientoInventario = dc.MantenimientoInventario.Where(a => a.IdMantenimientoInventario != null).ToList();
            }
            return View(ReporteMantenimientoInventario);
        }
    }
}
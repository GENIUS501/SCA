using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteMantenimientoVehiculoController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteMantenimientoVehiculo = new List<MantenimientoVehiculo>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteMantenimientoVehiculo = dc.MantenimientoVehiculo.Where(a => a.IdMantenimientoVehiculo != null).ToList();
            }
            return View(ReporteMantenimientoVehiculo);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteControlVehiculoController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteControlVehiculo = new List<ControlVehiculo>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteControlVehiculo = dc.ControlVehiculo.Where(a => a.IdControlVehiculo != null).ToList();
            }
            return View(ReporteControlVehiculo);
        }
    }
}
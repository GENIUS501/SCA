using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReportePersonalController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReportePersonal = new List<Personal>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReportePersonal = dc.Personal.Where(a => a.IdPersonal != null).ToList();
            }
                return View(ReportePersonal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReporteFlotillaController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
            var ReporteFlotilla = new List<Flotilla>();

            using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            {
                ReporteFlotilla = dc.Flotilla.Where(a => a.IdFlotilla != null).ToList();
            }
            return View(ReporteFlotilla);
        }
    }
}
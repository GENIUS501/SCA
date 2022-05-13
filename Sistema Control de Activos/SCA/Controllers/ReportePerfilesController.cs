using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class ReportePerfilesController : Controller
    {
        // GET: ReportePersonal
        public ActionResult List()
        {
           // var ReportePerfiles = new List<Perfiles>();

            //using (BaseDatosSCAEntities dc = new BaseDatosSCAEntities())
            //{
            //    ReportePerfiles = dc.Perfiles.Where(a => a.IdPerfiles != null).ToList();
            //}
            return View();
        }
    }
}
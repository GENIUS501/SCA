using Filters;
using SCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCA.Controllers
{
    public class BitacoraSesionController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        [AuthorizeUser(idmodulo: "BitacoraEntrada/Salida")]
        // GET: BitacoraSesion
        public ActionResult Index(string Nombre, string FechaIni, string FechaFin)
        {
            var Modelo = db.BitacoraIngresoSalida.ToList();
            if (Nombre != null)
            {
                Modelo = Modelo.Where(x => x.Usuario.Personal.Nombre.Contains(Nombre)).ToList();
            }
            if (FechaIni != null && FechaFin != null)
            {
                Modelo = Modelo.Where(x => x.FechaIngreso >= Convert.ToDateTime(FechaIni) && x.FechaIngreso <= Convert.ToDateTime(FechaFin)).ToList();
            }
            return View(Modelo);
        }
    }
}
using Filters;
using SCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCA.Controllers
{
    public class BitacoraMovimientosController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        [AuthorizeUser(idmodulo: "BitacoraMovimientos")]
        // GET: BitacoraMovimientos
        public ActionResult Index(string Nombre,string Tipo, string FechaIni, string FechaFin)
        {
            var Modelo = db.BitacoraMovimiento.ToList();
            if (Nombre != null)
            {
                Modelo = Modelo.Where(x => x.Usuario.Usuario1.Contains(Nombre)).ToList();
            }
            if (Tipo != null)
            {
                Modelo = Modelo.Where(x => x.TipoMovimiento.Contains(Tipo)).ToList();
            }
            if (FechaIni != null && FechaFin != null)
            {
                Modelo = Modelo.Where(x => x.FechaMovimiento >= Convert.ToDateTime(FechaIni) && x.FechaMovimiento <= Convert.ToDateTime(FechaFin)).ToList();
            }
            return View(Modelo);
        }
    }
}
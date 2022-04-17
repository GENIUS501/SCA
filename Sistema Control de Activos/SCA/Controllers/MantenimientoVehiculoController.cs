using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class MantenimientoVehiculoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: MantenimientoVehiculo
        public ActionResult Index()
        {
            var ManVeh = db.MantenimientoVehiculo.Include(a => a.Flotilla);
            return View(ManVeh.ToList());
        }

        // GET: MantenimientoVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
            if (mantenimientovehiculo == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientovehiculo);
        }

        // GET: MantenimientoVehiculo/Create
        public ActionResult Create()
        {
            ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa");
            return View();
        }

        // POST: MantenimientoVehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMantenimientoVehiculo, IdFlotilla, TipoMentenimiento, KilometrajeActual" +
                                                   "ProximoKilometraje, CostoMantenimiento, FechaMantenimiento " +
                                                   "DescripcionServicio")] MantenimientoVehiculo mantenimientovehiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MantenimientoVehiculo.Add(mantenimientovehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa");
                return View(mantenimientovehiculo);
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoInventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
            if (mantenimientovehiculo == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", mantenimientovehiculo.IdFlotilla);
            return View(mantenimientovehiculo);
        }

        // POST: MantenimientoInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMantenimientoVehiculo, IdFlotilla, TipoMentenimiento, KilometrajeActual" +
                                                   "ProximoKilometraje, CostoMantenimiento, FechaMantenimiento " +
                                                   "DescripcionServicio")] MantenimientoVehiculo mantenimientovehiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mantenimientovehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", mantenimientovehiculo.IdFlotilla);
                return View(mantenimientovehiculo);
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoInventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
            if (mantenimientovehiculo == null)
            {
                return HttpNotFound();
            }

            return View(mantenimientovehiculo);
        }

        // POST: MantenimientoInventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
                db.MantenimientoVehiculo.Remove(mantenimientovehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
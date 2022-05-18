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
    public class ControlVehiculoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: ControlVehiculo
        public ActionResult Index()
        {
            var Model = db.ControlVehiculo.Include(a => a.Flotilla).Include(a => a.Personal).ToList();
            return View(Model);
        }

        // GET: ControlVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }
            return View(controlvehiculo);
        }

        // GET: ControlVehiculo/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ControlVehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdControlVehiculo, IdFlotilla, IdPersonal, EstadoVehiculo" +
                                                   "FechaSalida, KilometrajeSalida, FechaIngresa " +
                                                   "KilometrajeIngresa, Anomalias")]ControlVehiculo controlvehiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ControlVehiculo.Add(controlvehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa");
                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre");
                return View(controlvehiculo);
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlInventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", controlvehiculo.IdFlotilla);
            ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre", controlvehiculo.IdPersonal);
            return View(controlvehiculo);
        }

        // POST: ControlInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdControlVehiculo, IdFlotilla, IdPersonal, EstadoVehiculo" +
                                                   "FechaSalida, KilometrajeSalida, FechaIngresa " +
                                                   "KilometrajeIngresa, Anomalias")]ControlVehiculo controlvehiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(controlvehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", controlvehiculo.IdFlotilla);
                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre", controlvehiculo.IdPersonal);
                return View(controlvehiculo);
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlInventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }

            return View(controlvehiculo);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
                db.ControlVehiculo.Remove(controlvehiculo);
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
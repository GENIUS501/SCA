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
    public class ControlInventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: ControlInventario
        public ActionResult Index()
        {
            var ColInv = db.ControlInventario.Include(a => a.Inventario);
            var CoIn = db.ControlInventario.Include(a => a.Personal);
            return View(ColInv.ToList());
        }

        // GET: ControlInventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlInventario controlinventario = db.ControlInventario.Find(id);
            if (controlinventario == null)
            {
                return HttpNotFound();
            }
            return View(controlinventario);
        }

        // GET: ControlInventario/Create
        public ActionResult Create()
        {
            ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre");
            ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre");
            return View();
        }

        // POST: ControlInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdControlInventario, IdInventario, IdPersonal, EstadoActivo" +
                                                   "FechaSalida, FechaIngresa, Anomalias")]ControlInventario controlinventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ControlInventario.Add(controlinventario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre");
                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre");
                return View(controlinventario);
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

            ControlInventario controlinventario = db.ControlInventario.Find(id);
            if (controlinventario == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre", controlinventario.IdInventario);
            ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre", controlinventario.IdPersonal);
            return View(controlinventario);
        }

        // POST: ControlInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdControlInventario, IdInventario, IdPersonal, EstadoActivo" +
                                                   "FechaSalida, FechaIngresa, Anomalias")]ControlInventario controlinventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(controlinventario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre", controlinventario.IdInventario);
                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre", controlinventario.IdPersonal);
                return View(controlinventario);
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

            ControlInventario controlinventario = db.ControlInventario.Find(id);
            if (controlinventario == null)
            {
                return HttpNotFound();
            }

            return View(controlinventario);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ControlInventario controlinventario = db.ControlInventario.Find(id);
                db.ControlInventario.Remove(controlinventario);
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
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
    public class MantenimientoInventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: MantenimientoInventario
        public ActionResult Index()
        {
            var ManInv = db.MantenimientoInventario.Include(a => a.Inventario);
            return View(ManInv.ToList());
        }

        // GET: MantenimientoInventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Find(id);
            if (mantenimientoinventario == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoinventario);
        }

        // GET: MantenimientoInventario/Create
        public ActionResult Create()
        {
            ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre");
            return View();
        }

        // POST: MantenimientoInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMantenimientoInventario, IdInventario, TipoMantenimiento" +
                                                   "CostoMantenimiento, FechaMantenimiento, FechaProximoMantenimiento" +
                                                   "DescripcionServicio")]MantenimientoInventario mantenimientoinventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MantenimientoInventario.Add(mantenimientoinventario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre");
                return View(mantenimientoinventario);
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

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Find(id);
            if (mantenimientoinventario == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre", mantenimientoinventario.IdInventario);
            return View(mantenimientoinventario);
        }

        // POST: MantenimientoInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMantenimientoInventario, IdInventario, TipoMantenimiento" +
                                                   "CostoMantenimiento, FechaMantenimiento, FechaProximoMantenimiento" +
                                                   "DescripcionServicio")]MantenimientoInventario mantenimientoinventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mantenimientoinventario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdInventario = new SelectList(db.Inventario, "IdInventario", "Nombre", mantenimientoinventario.IdInventario);
                return View(mantenimientoinventario);
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

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Find(id);
            if (mantenimientoinventario == null)
            {
                return HttpNotFound();
            }

            return View(mantenimientoinventario);
        }

        // POST: MantenimientoInventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Find(id);
                db.MantenimientoInventario.Remove(mantenimientoinventario);
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
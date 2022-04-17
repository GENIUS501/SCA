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
    public class PerfilesController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Perfiles
        public ActionResult Index()
        {
            var per = db.Perfiles.Include(a => a.Permisos);
            return View(per.ToList());
        }

        // GET: Perfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Perfiles perfiles = db.Perfiles.Find(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }
            return View(perfiles);
        }

        // GET: Perfiles/Create
        public ActionResult Create()
        {
            ViewBag.IdPermisos = new SelectList(db.Permisos, "IdPermisos", "Nombre");
            ViewBag.PRUEBA = "";
            return View();
        }

        // POST: Perfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPerfiles, Nombre, IdPermisos")]Perfiles perfiles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Perfiles.Add(perfiles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdPermisos = new SelectList(db.Permisos, "IdPermisos", "Nombre", perfiles.IdPermisos);
                return View(perfiles);
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Perfiles perfiles = db.Perfiles.Find(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdPermisos = new SelectList(db.Permisos, "IdPermisos", "Nombre", perfiles.IdPermisos);
            return View(perfiles);
        }

        // POST: Perfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPerfiles, Nombre, IdPermisos")]Perfiles perfiles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(perfiles).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdPermisos = new SelectList(db.Permisos, "IdPermisos", "Nombre", perfiles.IdPermisos);
                return View(perfiles);
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Perfiles perfiles = db.Perfiles.Find(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }

            return View(perfiles);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Perfiles perfiles = db.Perfiles.Find(id);
                db.Perfiles.Remove(perfiles);
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
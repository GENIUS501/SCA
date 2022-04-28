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
    public class LicenciaController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Licencia
        public ActionResult Index()
        {
            var lic = db.Licencia;
            return View(lic.ToList());
        }

        // GET: Licencia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }
            return View(licencia);
        }

        // GET: Licencia/Create
        public ActionResult Create()
        {
            ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula");
            return View();
        }

        // POST: Licencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLicencia, IdPersonal, TipoLicencia, VenceLicencia")]Licencia licencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Licencia.Add(licencia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula", licencia.IdPersonal);
                return View(licencia);
            }
            catch
            {
                return View();
            }
        }

        // GET: Licencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }

            //ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula", licencia.IdPersonal);
            return View(licencia);
        }

        // POST: Licencia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLicencia, IdPersonal, TipoLicencia, VenceLicencia")]Licencia licencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(licencia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula", licencia.IdPersonal);
                return View(licencia);
            }
            catch
            {
                return View();
            }
        }

        // GET: Licencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }

            return View(licencia);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Licencia licencia = db.Licencia.Find(id);
                db.Licencia.Remove(licencia);
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
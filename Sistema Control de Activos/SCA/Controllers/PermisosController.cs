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
    public class PermisosController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Permisos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: Permisos/Create
        public ActionResult Create()
        {
            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre");
            return View();
        }

        // POST: Permiso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPermisos, Nombre")] Permisos permisos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Permisos.Add(permisos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
                return View(permisos);
            }
            catch
            {
                return View();
            }
        }

        // GET: Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }

            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
            return View(permisos);
        }

        // POST: Permisos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPermisos, Nombre")] Permisos permisos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(permisos).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
                return View(permisos);
            }
            catch
            {
                return View();
            }
        }

        // GET: Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }

            return View(permisos);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Permisos permisos = db.Permisos.Find(id);
                db.Permisos.Remove(permisos);
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
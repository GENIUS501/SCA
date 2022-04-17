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
    public class DepartamentoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Departamento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Departamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre");
            return View();
        }

        // POST: Departamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDepartamento, Nombre")]Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departamento.Add(departamento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
                return View(departamento);
            }
            catch
            {
                return View();
            }
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDepartamento, Nombre")]Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(departamento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
                return View(departamento);
            }
            catch
            {
                return View();
            }
        }

        // GET: Departamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Departamento departamento = db.Departamento.Find(id);
                db.Departamento.Remove(departamento);
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
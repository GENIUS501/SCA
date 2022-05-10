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
    public class PersonalController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Personal
        public ActionResult Index()
        {
            var per = db.Personal.Include(a => a.Licencia).Include(x=>x.Departamento);
            return View(per.ToList());
        }

        // GET: Personal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personal/Create
        public ActionResult Create()
        {
            ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.IdDepartamento.ToString(),
                    Selected = false
                };
            });
            ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.TipoLicencia,
                    Value = d.IdLicencia.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: Personal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersonal, Cedula, Nombre, Apellido1, Apellido2" +
                                                   "Telefono, Correo, IdLicencia, CarnetMS, VenceCarnetMS" +
                                                   "IdDepartamento, MotivoDeshabilitar")]Personal personal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Personal.Add(personal);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", personal.IdDepartamento);
                ViewBag.IdLicencia = new SelectList(db.Licencia, "IdLicencia", "TipoLicencia", personal.IdLicencia);
                return View(personal);
            }
            catch
            {
                return View();
            }
        }

        // GET: Personal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", personal.IdDepartamento);
            ViewBag.IdLicencia = new SelectList(db.Licencia, "IdLicencia", "TipoLicencia", personal.IdLicencia);
            return View(personal);
        }

        // POST: Personal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersonal, Cedula, Nombre, Apellido1, Apellido2" +
                                                   "Telefono, Correo, IdLicencia, CarnetMS, VenceCarnetMS" +
                                                   "IdDepartamento, MotivoDeshabilitar")]Personal personal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(personal).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", personal.IdDepartamento);
                ViewBag.IdLicencia = new SelectList(db.Licencia, "IdLicencia", "TipoLicencia", personal.IdLicencia);
                return View(personal);
            }
            catch
            {
                return View();
            }
        }

        // GET: Personal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }

            return View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Personal personal = db.Personal.Find(id);
                db.Personal.Remove(personal);
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

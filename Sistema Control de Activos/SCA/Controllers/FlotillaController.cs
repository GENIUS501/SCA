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
    public class FlotillaController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Flotilla
        public ActionResult Index()
        {
            var flot = db.Flotilla.Include(a => a.Departamento);
            return View(flot.ToList());
        }

        // GET: Flotilla/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var flotillas = db.Flotilla.Include(x => x.Departamento).ToList();
            var flotilla = flotillas.Where(x => x.IdFlotilla == id).FirstOrDefault();
            if (flotilla == null)
            {
                return HttpNotFound();
            }
            return View(flotilla);
        }

        // GET: Flotilla/Create
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
            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre");
            return View();
        }

        // POST: Flotilla/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flotilla flotilla)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Flotilla.Add(flotilla);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdDepartamento.ToString(),
                        Selected = false
                    };
                });
                return View(flotilla);
            }
            catch
            {
                return View();
            }
        }

        // GET: Flotilla/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flotilla flotilla = db.Flotilla.Find(id);
            if (flotilla == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.IdDepartamento.ToString(),
                    Selected = false
                };
            });
            return View(flotilla);
        }

        // POST: Flotilla/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Flotilla flotilla)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(flotilla).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdDepartamento.ToString(),
                        Selected = false
                    };
                });
                return View(flotilla);
            }
            catch
            {
                return View();
            }
        }

        // GET: Flotilla/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flotilla flotilla = db.Flotilla.Find(id);
            if (flotilla == null)
            {
                return HttpNotFound();
            }

            return View(flotilla);
        }

        // POST: Flotilla/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Flotilla flotilla = db.Flotilla.Find(id);
                db.Flotilla.Remove(flotilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

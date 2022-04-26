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
    public class InventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Inventario
        public ActionResult Index()
        {
            var inv = db.Inventario.Include(a => a.Departamento);
            return View(inv.ToList());
        }

        // GET: Inventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventario inventario = db.Inventario.Find(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }
            return View(inventario);
        }

        // GET: Inventario/Create
        public ActionResult Create()
        {
            ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.IdDepartamento.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdInventario, CodigoEmpresa, Nombre, Modelo, Serie" +
                                                   "Fabricante, FechaCompra, CostoEquipo, Garantia, VenceGarantia" +
                                                   "IdDepartamento, MotivoDeshabilitar")]Inventario inventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Inventario.Add(inventario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", inventario.IdDepartamento);
                return View(inventario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventario inventario = db.Inventario.Find(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", inventario.IdDepartamento);
            return View(inventario);
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdInventario, CodigoEmpresa, Nombre, Modelo, Serie" +
                                                   "Fabricante, FechaCompra, CostoEquipo, Garantia, VenceGarantia" +
                                                   "IdDepartamento, MotivoDeshabilitar")]Inventario inventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(inventario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", inventario.IdDepartamento);
                return View(inventario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventario inventario = db.Inventario.Find(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }

            return View(inventario);
        }

        // POST: Inventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Inventario inventario = db.Inventario.Find(id);
                db.Inventario.Remove(inventario);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SCA.Models;

namespace SCA.Controllers
{
    public class MantenimientoVehiculoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: MantenimientoVehiculo
        public ActionResult Index()
        {
            var ManVeh = db.MantenimientoVehiculo.Include(a => a.Flotilla);
            return View(ManVeh.ToList());
        }

        // GET: MantenimientoVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
            if (mantenimientovehiculo == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientovehiculo);
        }

        // GET: MantenimientoVehiculo/Create
        public ActionResult Create()
        {
            ViewBag.IdFlotilla = db.Flotilla.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa.ToString(),
                    Value = d.IdFlotilla.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: MantenimientoVehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoVehiculo Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            db.MantenimientoInventario.Add(Modelo);
                            int Resultado = db.SaveChanges();
                            if (Resultado > 0)
                            {
                                Ts.Complete();
                                var UsuarioLogueado = (Usuario)Session["User"];
                                Helpers.Helper.RegistrarMovimiento("Agrego", "MantenimientoInventario", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                                TempData["msg"] = "<script>alert('Mantenimiento Inventario Agregada exitosamente!!');</script>";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                Ts.Dispose();
                                TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de inventario!!');</script>";
                                ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
                                {
                                    return new SelectListItem()
                                    {
                                        Text = d.Nombre,
                                        Value = d.IdInventario.ToString(),
                                        Selected = false
                                    };
                                });
                                return View(Modelo);
                            }
                        }
                    }
                    TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de inventario!!');</script>";
                    db.MantenimientoVehiculo.Add(Modelo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa");
                return View(Modelo);
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

            MantenimientoVehiculo Modelo = db.MantenimientoVehiculo.Find(id);
            if (Modelo == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", Modelo.IdFlotilla);
            return View(Modelo);
        }

        // POST: MantenimientoInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MantenimientoVehiculo Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(Modelo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", Modelo.IdFlotilla);
                return View(Modelo);
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

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
            if (mantenimientovehiculo == null)
            {
                return HttpNotFound();
            }

            return View(mantenimientovehiculo);
        }

        // POST: MantenimientoInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Find(id);
                db.MantenimientoVehiculo.Remove(mantenimientovehiculo);
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
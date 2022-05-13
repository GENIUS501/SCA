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

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Include(x => x.Flotilla).Where(x=>x.IdFlotilla==id).FirstOrDefault();
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
                            db.MantenimientoVehiculo.Add(Modelo);
                            int Resultado = db.SaveChanges();
                            if (Resultado > 0)
                            {
                                Ts.Complete();
                                var UsuarioLogueado = (Usuario)Session["User"];
                                Helpers.Helper.RegistrarMovimiento("Agrego", "MantenimientoVehiculo", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                                TempData["msg"] = "<script>alert('Mantenimiento vehiculo Agregada exitosamente!!');</script>";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                Ts.Dispose();
                                TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de vehiculo!!');</script>";
                                ViewBag.IdFlotilla = db.Flotilla.ToList().ConvertAll(d =>
                                {
                                    return new SelectListItem()
                                    {
                                        Text = d.Placa.ToString(),
                                        Value = d.IdFlotilla.ToString(),
                                        Selected = false
                                    };
                                });
                                return View(Modelo);
                            }
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de vehiculo!!');</script>";
                ViewBag.IdFlotilla = db.Flotilla.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Placa.ToString(),
                        Value = d.IdFlotilla.ToString(),
                        Selected = false
                    };
                });
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de vehiculo!!');</script>";
                return RedirectToAction("Index");
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

            ViewBag.IdFlotilla = db.Flotilla.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa.ToString(),
                    Value = d.IdFlotilla.ToString(),
                    Selected = false
                };
            });
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
                    var ValorAntiguo = db.MantenimientoVehiculo.Where(x => x.IdMantenimientoVehiculo == Modelo.IdMantenimientoVehiculo).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.MantenimientoVehiculo.Where(x => x.IdMantenimientoVehiculo == Modelo.IdMantenimientoVehiculo).FirstOrDefault();
                        Objbd.IdFlotilla = Modelo.IdFlotilla;
                        Objbd.CostoMantenimiento = Modelo.CostoMantenimiento;
                        Objbd.DescripcionServicio = Modelo.DescripcionServicio;
                        Objbd.FechaMantenimiento = Modelo.FechaMantenimiento;
                        Objbd.IdMantenimientoVehiculo = Modelo.IdMantenimientoVehiculo;
                        Objbd.KilometrajeActual = Modelo.KilometrajeActual;
                        Objbd.ProximoKilometraje = Modelo.ProximoKilometraje;
                        Objbd.TipoMantenimiento = Modelo.TipoMantenimiento;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "MantenimientoVehiculo", Modelo.ValorAntiguo(ValorAntiguo), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('MantenimientoVehiculo editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdInventario.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al editar el mantenimientoVehiculo!!');</script>";
                            return View(Modelo);
                        }
                    }
                }
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
            catch
            {
                TempData["msg"] = "<script>alert('MantenimientoVehiculo editado exitosamente!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: MantenimientoInventario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoVehiculo mantenimientovehiculo = db.MantenimientoVehiculo.Include(x => x.Flotilla).Where(x => x.IdFlotilla == id).FirstOrDefault();
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
                var ValorAntiguo = db.MantenimientoVehiculo.Where(x => x.IdFlotilla == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var Objbd = db.MantenimientoVehiculo.Where(x => x.IdFlotilla == id).FirstOrDefault();
                    db.MantenimientoVehiculo.Remove(Objbd);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "MantenimientoVehiculo", Objbd.ValorAntiguo(ValorAntiguo), "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('mantenimiento vehiculo eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar el mantenimiento vehiculo!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar el mantenimiento vehiculo!!');</script>";
                return RedirectToAction("Index");
            }
        }

    }
}
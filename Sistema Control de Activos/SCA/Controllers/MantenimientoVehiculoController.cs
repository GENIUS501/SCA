﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Filters;
using SCA.Models;

namespace SCA.Controllers
{
    public class MantenimientoVehiculoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: MantenimientoVehiculo
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
        public ActionResult Index(string Placa, string Marca, string IdDepartamento, string Tipo, string FechaIni, string FechaFin)
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
            var Modelo = db.MantenimientoVehiculo.Include(a => a.Flotilla).ToList();
            if (Placa != null)
            {
                int id = int.Parse(Placa);
                Modelo = Modelo.Where(x => x.Flotilla.Placa == id).ToList();
            }
            if (Marca != null)
            {
                Modelo = Modelo.Where(x => x.Flotilla.Marca.Contains(Marca)).ToList();
            }
            if (IdDepartamento != null)
            {
                int id = int.Parse(IdDepartamento);
                Modelo = Modelo.Where(x => x.Flotilla.IdDepartamento == id).ToList();
            }
            if (Tipo != null)
            {
                int id = int.Parse(Tipo);
                Modelo = Modelo.Where(x => x.TipoMantenimiento == id).ToList();
            }
            if (FechaIni != null && FechaFin != null)
            {
                Modelo = Modelo.Where(x => x.FechaMantenimiento >= Convert.ToDateTime(FechaIni) && x.FechaMantenimiento <= Convert.ToDateTime(FechaFin)).ToList();
            }
            return View(Modelo);
        }

        // GET: MantenimientoVehiculo/Details/5
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
        public ActionResult Details(int? id)
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

        // GET: MantenimientoVehiculo/Create
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
        [AuthorizeUser(idmodulo: "MantenimientoVehículos")]
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
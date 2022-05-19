using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SCA.Models;

namespace SCA.Controllers
{
    public class ControlVehiculoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: ControlVehiculo
        public ActionResult Index()
        {
            var Model = db.ControlVehiculo.Include(a => a.Flotilla).Include(a => a.Personal).ToList();
            return View(Model);
        }

        // GET: ControlVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Include(a => a.Flotilla).Include(a => a.Personal).Where(x => x.IdControlVehiculo == id).FirstOrDefault();
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }
            return View(controlvehiculo);
        }

        // GET: ControlVehiculo/Create
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
            ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + d.Apellido1 + d.Apellido2,
                    Value = d.IdPersonal.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: ControlVehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControlVehiculoViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            ControlVehiculo Objbd = new ControlVehiculo();
                            Objbd.Anomalias = Modelo.Anomalias;
                            Objbd.EstadoVehiculo = Modelo.EstadoVehiculo;
                            Objbd.FechaIngresa = Modelo.FechaIngresa;
                            Objbd.FechaSalida = Modelo.FechaSalida;
                            Objbd.IdFlotilla = Modelo.IdFlotilla;
                            Objbd.IdPersonal = Modelo.IdPersonal;
                            Objbd.KilometrajeIngresa = Modelo.KilometrajeIngresa;
                            Objbd.KilometrajeSalida = Modelo.KilometrajeSalida;
                            db.ControlVehiculo.Add(Objbd);
                            int Resultado = db.SaveChanges();
                            if (Resultado > 0)
                            {
                                Ts.Complete();
                                var UsuarioLogueado = (Usuario)Session["User"];
                                Helpers.Helper.RegistrarMovimiento("Agrego", "ControlVehiculo", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                                TempData["msg"] = "<script>alert('Control de vehículo Agregado exitosamente!!');</script>";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                Ts.Dispose();
                                TempData["msg"] = "<script>alert('Error al agregar el control de vehículo!!');</script>";
                                return View(Modelo);
                            }
                        }
                    }
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
                ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre + d.Apellido1 + d.Apellido2,
                        Value = d.IdPersonal.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al agregar el control de vehículo!!');</script>";
                return View(Modelo);
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Error al agregar el control de vehículo!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: ControlInventario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Where(x => x.IdControlVehiculo == id).FirstOrDefault();
            ControlVehiculoViewModel Modelo = new ControlVehiculoViewModel();
            Modelo.Anomalias = controlvehiculo.Anomalias;
            Modelo.EstadoVehiculo = controlvehiculo.EstadoVehiculo;
            Modelo.FechaIngresa = controlvehiculo.FechaIngresa;
            Modelo.FechaSalida = controlvehiculo.FechaSalida;
            Modelo.IdFlotilla = controlvehiculo.IdFlotilla;
            Modelo.IdPersonal = controlvehiculo.IdPersonal;
            Modelo.KilometrajeIngresa = controlvehiculo.KilometrajeIngresa;
            Modelo.KilometrajeSalida = controlvehiculo.KilometrajeSalida;
            Modelo.IdControlVehiculo = controlvehiculo.IdControlVehiculo;
            if (controlvehiculo == null)
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
            ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + d.Apellido1 + d.Apellido2,
                    Value = d.IdPersonal.ToString(),
                    Selected = false
                };
            });
            return View(Modelo);
        }

        // POST: ControlInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ControlVehiculoViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguoEntidad = db.ControlVehiculo.Where(x => x.IdControlVehiculo == Modelo.IdControlVehiculo).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.ControlVehiculo.Where(x => x.IdControlVehiculo == Modelo.IdControlVehiculo).FirstOrDefault();
                        Objbd.Anomalias = Modelo.Anomalias;
                        Objbd.EstadoVehiculo = Modelo.EstadoVehiculo;
                        Objbd.FechaIngresa = Modelo.FechaIngresa;
                        Objbd.FechaSalida = Modelo.FechaSalida;
                        Objbd.IdFlotilla = Modelo.IdFlotilla;
                        Objbd.IdPersonal = Modelo.IdPersonal;
                        Objbd.KilometrajeIngresa = Modelo.KilometrajeIngresa;
                        Objbd.KilometrajeSalida = Modelo.KilometrajeSalida;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "ControlVehiculo", Modelo.ValorAntiguo(ValorAntiguoEntidad), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Control de vehículo editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al editar el control de vehículo!!');</script>";
                            return View(Modelo);
                        }
                    }
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
                ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre + d.Apellido1 + d.Apellido2,
                        Value = d.IdPersonal.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al editar el control de vehículo!!');</script>";
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar el control de vehículo!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: ControlInventario/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Include(a => a.Flotilla).Include(a => a.Personal).Where(x => x.IdControlVehiculo == id).FirstOrDefault();
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }
            return View(controlvehiculo);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                int ida = int.Parse(id);
                ControlVehiculoViewModel Modelo = new ControlVehiculoViewModel();
                var ValorAntiguoEntidad = db.ControlVehiculo.Where(x => x.IdControlVehiculo == ida).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    ControlVehiculo Objbd = db.ControlVehiculo.Where(x => x.IdControlVehiculo == ida).FirstOrDefault();
                    db.ControlVehiculo.Remove(Objbd);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "ControlVehiculo", Modelo.ValorAntiguo(ValorAntiguoEntidad), "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Control de vehículo eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar Control de vehículo!!');</script>";
                        return View(Objbd);
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar Control de vehículo!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}
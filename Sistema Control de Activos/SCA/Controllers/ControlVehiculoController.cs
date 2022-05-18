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

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
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
            catch(Exception)
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

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
            var ControlJson = JsonConvert.SerializeObject(controlvehiculo);
            var Modelo = JsonConvert.DeserializeObject<ControlVehiculoViewModel>(ControlJson);
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
                    db.Entry(Modelo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdFlotilla = new SelectList(db.Flotilla, "IdFlotilla", "Placa", Modelo.IdFlotilla);
                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Nombre", Modelo.IdPersonal);
                return View(Modelo);
            }
            catch
            {
                return View();
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

            ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
            var ControlJson = JsonConvert.SerializeObject(controlvehiculo);
            var Modelo = JsonConvert.DeserializeObject<ControlVehiculoViewModel>(ControlJson);
            if (controlvehiculo == null)
            {
                return HttpNotFound();
            }

            return View(Modelo);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                ControlVehiculo controlvehiculo = db.ControlVehiculo.Find(id);
                db.ControlVehiculo.Remove(controlvehiculo);
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
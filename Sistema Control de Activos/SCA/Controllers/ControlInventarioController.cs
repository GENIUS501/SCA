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
    public class ControlInventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: ControlInventario
        public ActionResult Index()
        {
            var Modelo = db.ControlInventario.Include(a => a.Inventario).Include(a => a.Personal);
            return View(Modelo.ToList());
        }

        // GET: ControlInventario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControlInventario Modelo = db.ControlInventario.Include(a => a.Inventario).Include(a => a.Personal).Where(x=>x.IdControlInventario==id).FirstOrDefault();
            if (Modelo == null)
            {
                return HttpNotFound();
            }
            return View(Modelo);
        }

        // GET: ControlInventario/Create
        public ActionResult Create()
        {
            ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.IdInventario.ToString(),
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

        // POST: ControlInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControlInventarioViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        ControlInventario  Objbd = new ControlInventario();
                        Objbd.Anomalias = Modelo.Anomalias;
                        Objbd.IdInventario = Modelo.IdInventario;
                        Objbd.FechaIngresa = Modelo.FechaIngresa;
                        Objbd.FechaSalida = Modelo.FechaSalida;
                        Objbd.IdPersonal = Modelo.IdPersonal;
                        Objbd.EstadoActivo = Modelo.EstadoActivo;
                        db.ControlInventario.Add(Objbd);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "ControlInventario", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Control de inventario Agregado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al agregar el control de inventario!!');</script>";
                            ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdInventario.ToString(),
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
                ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre + d.Apellido1 + d.Apellido2,
                        Value = d.IdPersonal.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al agregar el control de inventario!!');</script>";
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar el control de inventario!!');</script>";
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

            ControlInventario controlinventario = db.ControlInventario.Find(id);
            ControlInventarioViewModel Modelo = new ControlInventarioViewModel();
            Modelo.Anomalias = controlinventario.Anomalias;
            Modelo.IdInventario = controlinventario.IdInventario;
            Modelo.FechaIngresa = controlinventario.FechaIngresa;
            Modelo.FechaSalida = controlinventario.FechaSalida;
            Modelo.IdPersonal = controlinventario.IdPersonal;
            Modelo.EstadoActivo = controlinventario.EstadoActivo;
            Modelo.IdControlInventario = controlinventario.IdControlInventario;
            if (controlinventario == null)
            {
                return HttpNotFound();
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
        public ActionResult Edit(ControlInventarioViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguoEntidad = db.ControlInventario.Where(x => x.IdControlInventario == Modelo.IdControlInventario).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.ControlInventario.Where(x => x.IdControlInventario == Modelo.IdControlInventario).FirstOrDefault();
                        Objbd.Anomalias = Modelo.Anomalias;
                        Objbd.IdInventario = Modelo.IdInventario;
                        Objbd.FechaIngresa = Modelo.FechaIngresa;
                        Objbd.FechaSalida = Modelo.FechaSalida;
                        Objbd.IdPersonal = Modelo.IdPersonal;
                        Objbd.EstadoActivo = Modelo.EstadoActivo;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "ControlInventario", Modelo.ValorAntiguo(ValorAntiguoEntidad), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Control de inventario editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al editar el control de inventario!!');</script>";
                            ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdInventario.ToString(),
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
                    }
                }
                TempData["msg"] = "<script>alert('Error al editar el control de inventario!!');</script>";
                ViewBag.IdInventario = db.Inventario.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdInventario.ToString(),
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
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar el control de inventario!!');</script>";
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

            ControlInventario Modelo = db.ControlInventario.Include(a => a.Inventario).Include(a => a.Personal).Where(x => x.IdControlInventario == id).FirstOrDefault();
            if (Modelo == null)
            {
                return HttpNotFound();
            }

            return View(Modelo);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ControlInventarioViewModel Modelo = new ControlInventarioViewModel();
                var ValorAntiguoEntidad = db.ControlInventario.Where(x => x.IdControlInventario == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    ControlInventario Objbd = db.ControlInventario.Where(x => x.IdControlInventario == id).FirstOrDefault();
                    db.ControlInventario.Remove(Objbd);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "ControlInventario", Modelo.ValorAntiguo(ValorAntiguoEntidad), "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Control de inventario eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar Control de inventario!!');</script>";
                        return View(Objbd);
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar Control de inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }

    }
}
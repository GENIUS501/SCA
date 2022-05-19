using System;
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
    public class MantenimientoInventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: MantenimientoInventario
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Index()
        {
            var ManInv = db.MantenimientoInventario.Include(a => a.Inventario);
            return View(ManInv.ToList());
        }

        // GET: MantenimientoInventario/Details/5
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Include(a => a.Inventario).Where(x=>x.IdMantenimientoInventario==id).FirstOrDefault();
            if (mantenimientoinventario == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientoinventario);
        }

        // GET: MantenimientoInventario/Create
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
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
            return View();
        }

        // POST: MantenimientoInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Create(MantenimientoInventario Modelo)
        {
            try
            {
                if (ModelState.IsValid)
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
                TempData["msg"] = "<script>alert('Error al agregar el mantenimiento de inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: MantenimientoInventario/Edit/5
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Include(a => a.Inventario).Where(x=>x.IdMantenimientoInventario==id).FirstOrDefault();
            if (mantenimientoinventario == null)
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
            return View(mantenimientoinventario);
        }

        // POST: MantenimientoInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Edit(MantenimientoInventario Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguo = db.MantenimientoInventario.Where(x => x.IdInventario == Modelo.IdMantenimientoInventario).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.MantenimientoInventario.Where(x => x.IdInventario == Modelo.IdMantenimientoInventario).FirstOrDefault();
                        Objbd.IdInventario = Modelo.IdInventario;
                        Objbd.IdMantenimientoInventario = Modelo.IdMantenimientoInventario;
                        Objbd.CostoMantenimiento = Modelo.CostoMantenimiento;
                        Objbd.DescripcionServicio = Modelo.DescripcionServicio;
                        Objbd.FechaMantenimiento = Modelo.FechaMantenimiento;
                        Objbd.FechaProximoMantenimiento = Modelo.FechaProximoMantenimiento;
                        Objbd.TipoMantenimiento = Modelo.TipoMantenimiento;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "MantenimientoInventario", Modelo.ValorAntiguo(ValorAntiguo), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('MantenimientoInventario editado exitosamente!!');</script>";
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
                            TempData["msg"] = "<script>alert('Error al editar el mantenimientoInventario!!');</script>";
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
                TempData["msg"] = "<script>alert('Error al editar el mantenimientoInventario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: MantenimientoInventario/Delete/5
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MantenimientoInventario mantenimientoinventario = db.MantenimientoInventario.Find(id);
            if (mantenimientoinventario == null)
            {
                return HttpNotFound();
            }

            return View(mantenimientoinventario);
        }

        // POST: MantenimientoInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "MantenimientoInventario")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguo = db.MantenimientoInventario.Where(x => x.IdMantenimientoInventario == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var persona = db.MantenimientoInventario.Where(x => x.IdMantenimientoInventario == id).FirstOrDefault();
                    db.MantenimientoInventario.Remove(persona);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "MantenimientoInventario", persona.ValorAntiguo(ValorAntiguo), "", UsuarioLogueado.IdUsuario); 
                        TempData["msg"] = "<script>alert('mantenimiento inventario eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar el mantenimiento inventario!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar el mantenimiento inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}
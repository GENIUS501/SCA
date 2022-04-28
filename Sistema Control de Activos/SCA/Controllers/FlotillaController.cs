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
    public class FlotillaController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Flotilla
        public ActionResult Index()
        {
            try
            {
                var flot = db.Flotilla.Include(a => a.Departamento);
                return View(flot.ToList());
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Flotilla/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Flotilla/Create
        public ActionResult Create()
        {
            try
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
            catch (Exception)
            {
                return View();
            }
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
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Flotilla.Add(flotilla);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Flotilla", "", flotilla.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Flotilla Agregada exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdDepartamento.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al agregar flotilla!!');</script>";
                            return View(flotilla);
                        }
                    }
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
                TempData["msg"] = "<script>alert('Error al agregar flotilla!!');</script>";
                return View(flotilla);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar flotilla!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Flotilla/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
            catch (Exception)
            {
                return View();
            }
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
                    var ValorAntiguo = db.Flotilla.Where(x => x.IdFlotilla == flotilla.IdFlotilla).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var flotillaeditado = db.Flotilla.Where(x => x.IdFlotilla == flotilla.IdFlotilla).FirstOrDefault();
                        flotillaeditado.Ano = flotilla.Ano;
                        flotillaeditado.Combustible = flotilla.Combustible;
                        flotillaeditado.ControlVehiculo = flotilla.ControlVehiculo;
                        flotillaeditado.CostoVehiculo = flotilla.CostoVehiculo;
                        flotillaeditado.FechaCompra = flotilla.FechaCompra;
                        flotillaeditado.IdDepartamento = flotilla.IdDepartamento;
                        flotillaeditado.IdFlotilla = flotilla.IdFlotilla;
                        flotillaeditado.Marca = flotilla.Marca;
                        flotillaeditado.Modelo = flotilla.Modelo;
                        flotillaeditado.Placa = flotilla.Placa;
                        flotillaeditado.Traccion = flotilla.Traccion;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Flotilla", flotilla.ValorAntiguo(ValorAntiguo), flotilla.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Flotilla editada exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdDepartamento.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al editar flotilla!!');</script>";
                            return View(flotilla);
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al editar flotilla!!');</script>";
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
                TempData["msg"] = "<script>alert('Error al editar flotilla!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Flotilla/Delete/5
        public ActionResult Delete(int? id)
        {
            try
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
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Flotilla/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguo = db.Flotilla.Where(x => x.IdFlotilla == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Flotilla flotilla = db.Flotilla.Find(id);
                    db.Flotilla.Remove(flotilla);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Flotilla", flotilla.ValorAntiguo(ValorAntiguo), flotilla.ValorNuevo(), UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Flotilla eliminada exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar flotilla!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar flotilla!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}

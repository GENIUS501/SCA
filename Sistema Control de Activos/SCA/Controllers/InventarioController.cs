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
    public class InventarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Inventario
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Index()
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
            var inv = db.Inventario.Include(a => a.Departamento);
            return View(inv.ToList());
        }

        // GET: Inventario/Details/5
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inventarios = db.Inventario.Include(x => x.Departamento).ToList();
            Inventario inventario = inventarios.Where(x => x.IdInventario == id).FirstOrDefault();
            if (inventario == null)
            {
                return HttpNotFound();
            }
            return View(inventario);
        }

        // GET: Inventario/Create
        [AuthorizeUser(idmodulo: "Inventario")]
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
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Create(Inventario inventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Inventario.Add(inventario);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Inventario", "", inventario.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Inventario Agregado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdDepartamento.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al agregar inventario!!');</script>";
                            return View(inventario);
                        }
                    }
                }
                ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdDepartamento.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al agregar inventario!!');</script>";
                return View(inventario);
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Error al agregar inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Inventario/Edit/5
        [AuthorizeUser(idmodulo: "Inventario")]
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
            ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.IdDepartamento.ToString(),
                    Selected = false
                };
            });
            return View(inventario);
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Edit(Inventario inventario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguoEntidad = db.Inventario.Where(x => x.IdInventario == inventario.IdInventario).FirstOrDefault();
                    string ValorAntiguo = "IdInventario:" + ValorAntiguoEntidad.IdInventario.ToString() + " CodigoEmpresa:" + ValorAntiguoEntidad.CodigoEmpresa + " Nombre:" + ValorAntiguoEntidad.Nombre + " Modelo:" + ValorAntiguoEntidad.Modelo + " Serie:" + ValorAntiguoEntidad.Serie + " Fabricante:" + ValorAntiguoEntidad.Fabricante + " FechaCompra" + ValorAntiguoEntidad.FechaCompra.ToString() + " CostoEquipo:" + ValorAntiguoEntidad.CostoEquipo + " Garantia:" + ValorAntiguoEntidad.Garantia.ToString() + " VenceGarantia:" + ValorAntiguoEntidad.VenceGarantia + " IdDepartamento:" + ValorAntiguoEntidad.IdDepartamento;
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var inventarioguardar = db.Inventario.Find(inventario.IdInventario);
                        inventarioguardar.CodigoEmpresa = inventario.CodigoEmpresa;
                        inventarioguardar.ControlInventario = inventario.ControlInventario;
                        inventarioguardar.CostoEquipo = inventario.CostoEquipo;
                        inventarioguardar.Fabricante = inventario.Fabricante;
                        inventarioguardar.FechaCompra = inventario.FechaCompra;
                        inventarioguardar.Garantia = inventario.Garantia;
                        inventarioguardar.IdDepartamento = inventario.IdDepartamento;
                        inventarioguardar.IdInventario = inventario.IdInventario;
                        inventarioguardar.Modelo = inventario.Modelo;
                        inventarioguardar.Nombre = inventario.Nombre;
                        inventarioguardar.Serie = inventario.Serie;
                        inventarioguardar.VenceGarantia = inventario.VenceGarantia;
                        //db.Entry(inventarioguardar).State = EntityState.Modified;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Inventario", ValorAntiguo, inventario.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Inventario editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.Nombre,
                                    Value = d.IdDepartamento.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al editar inventario!!');</script>";
                            return View(inventario);
                        }
                    }
                }
                ViewBag.ListaDepartamento = db.Departamento.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdDepartamento.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al editar inventario!!');</script>";
                return View(inventario);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "<script>alert('Error al editar inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Inventario/Delete/5
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventarios = db.Inventario.Include(x => x.Departamento).ToList();
            Inventario inventario = inventarios.Where(x => x.IdInventario == id).FirstOrDefault();
            if (inventario == null)
            {
                return HttpNotFound();
            }

            return View(inventario);
        }

        // POST: Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Inventario")]
        public ActionResult Delete(int id)
        {
            try
            {

                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Inventario inventario = db.Inventario.Find(id);
                    var ValorAntiguoEntidad = db.Inventario.Find(inventario.IdInventario);
                    db.Inventario.Remove(inventario);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        string ValorAntiguo = "IdInventario:" + ValorAntiguoEntidad.IdInventario.ToString() + " CodigoEmpresa:" + ValorAntiguoEntidad.CodigoEmpresa + " Nombre:" + ValorAntiguoEntidad.Nombre + " Modelo:" + ValorAntiguoEntidad.Modelo + " Serie:" + ValorAntiguoEntidad.Serie + " Fabricante:" + ValorAntiguoEntidad.Fabricante + " FechaCompra" + ValorAntiguoEntidad.FechaCompra.ToString() + " CostoEquipo:" + ValorAntiguoEntidad.CostoEquipo + " Garantia:" + ValorAntiguoEntidad.Garantia.ToString() + " VenceGarantia:" + ValorAntiguoEntidad.VenceGarantia + " IdDepartamento:" + ValorAntiguoEntidad.IdDepartamento;
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Inventario", ValorAntiguo, "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Inventario eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar inventario!!');</script>";
                        return View(inventario);
                    }
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Error al eliminar inventario!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}

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
    public class PersonalController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Personal
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Index(string Nombre, string IdDepartamento)
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
            var per = db.Personal.Include(a => a.Licencia).Include(x => x.Departamento).ToList();
            if (Nombre != null)
            {
                per = per.Where(x => x.Nombre.Contains(Nombre)).ToList();
            }
            if(IdDepartamento!=null)
            {
                int id = int.Parse(IdDepartamento);
                per = per.Where(x => x.IdDepartamento==id).ToList();
            }
            return View(per);
        }

        // GET: Personal/Details/5
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Include(a => a.Licencia).Include(x => x.Departamento).FirstOrDefault(x => x.IdPersonal == id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personal/Create
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Create()
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
            ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.TipoLicencia,
                    Value = d.IdLicencia.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: Personal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Create(Personal Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Personal.Add(Modelo);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Personal", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Persona Agregada exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al agregar la persona!!');</script>";
                            return View(Modelo);
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al agregar la persona!!');</script>";
                ViewBag.IdDepartamento = db.Departamento.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Nombre,
                        Value = d.IdDepartamento.ToString(),
                        Selected = false
                    };
                });
                ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.TipoLicencia,
                        Value = d.IdLicencia.ToString(),
                        Selected = false
                    };
                });
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar la persona!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Personal/Edit/5
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Find(id);
            if (personal == null)
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
            ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.TipoLicencia,
                    Value = d.IdLicencia.ToString(),
                    Selected = false
                };
            });
            return View(personal);
        }

        // POST: Personal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Edit(Personal Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguo = db.Personal.Where(x => x.IdPersonal == Modelo.IdPersonal).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.Personal.Where(x => x.IdPersonal == Modelo.IdPersonal).FirstOrDefault();
                        Objbd.IdLicencia = Modelo.IdLicencia;
                        Objbd.Nombre = Modelo.Nombre;
                        Objbd.Apellido1 = Modelo.Apellido1;
                        Objbd.Apellido2 = Modelo.Apellido2;
                        Objbd.IdPersonal = Modelo.IdPersonal;
                        Objbd.IdDepartamento = Modelo.IdDepartamento;
                        Objbd.Cedula = Modelo.Cedula;
                        Objbd.CarnetMS = Modelo.CarnetMS;
                        Objbd.Correo = Modelo.Correo;
                        Objbd.VenceCarnetMS = Modelo.VenceCarnetMS;
                        Objbd.MotivoDeshabilitar = Modelo.MotivoDeshabilitar;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Personal", Modelo.ValorAntiguo(ValorAntiguo), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Persona editada exitosamente!!');</script>";
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
                            ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
                            {
                                return new SelectListItem()
                                {
                                    Text = d.TipoLicencia,
                                    Value = d.IdLicencia.ToString(),
                                    Selected = false
                                };
                            });
                            TempData["msg"] = "<script>alert('Error al editar la persona!!');</script>";
                            return View(Modelo);
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
                ViewBag.IdLicencia = db.Licencia.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.TipoLicencia,
                        Value = d.IdLicencia.ToString(),
                        Selected = false
                    };
                });
                TempData["msg"] = "<script>alert('Error al editar la persona!!');</script>";
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar la persona!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Personal/Delete/5
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }

            return View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Personal")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguo = db.Personal.Where(x => x.IdPersonal == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var persona = db.Personal.Where(x => x.IdPersonal == id).FirstOrDefault();
                    db.Personal.Remove(persona);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Personal", persona.ValorAntiguo(ValorAntiguo), "", UsuarioLogueado.IdUsuario); ;
                        TempData["msg"] = "<script>alert('Persona eliminada exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar la persona!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar la persona!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}

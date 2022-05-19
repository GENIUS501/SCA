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
    public class LicenciaController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Licencia
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Index()
        {
            var lic = db.Licencia;
            return View(lic.ToList());
        }

        // GET: Licencia/Details/5
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }
            return View(licencia);
        }

        // GET: Licencia/Create
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Licencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Create(/*[Bind(Include = "IdLicencia, IdPersonal, TipoLicencia, VenceLicencia")]*/Licencia licencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Licencia.Add(licencia);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Licencia", "", licencia.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Licencia Agregada exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al agregar la licencia!!');</script>";
                            return View(licencia);
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al agregar la licencia!!');</script>";
                //ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula", licencia.IdPersonal);
                return View(licencia);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar la licencia!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Licencia/Edit/5
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }

            //ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula", licencia.IdPersonal);
            return View(licencia);
        }

        // POST: Licencia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Edit(/*[Bind(Include = "IdLicencia, IdPersonal, TipoLicencia, VenceLicencia")]*/Licencia licencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguo = db.Licencia.Where(x => x.IdLicencia == licencia.IdLicencia).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Objbd = db.Licencia.Where(x => x.IdLicencia == licencia.IdLicencia).FirstOrDefault();
                        Objbd.IdLicencia = licencia.IdLicencia;
                        Objbd.TipoLicencia = licencia.TipoLicencia;
                        Objbd.VenceLicencia = licencia.VenceLicencia;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Licencia", licencia.ValorAntiguo(ValorAntiguo), licencia.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Licencia editada exitosamente!!');</script>";
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
                            TempData["msg"] = "<script>alert('Error al editar la licencia!!');</script>";
                            return View(licencia);
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al editar la licencia!!');</script>";
                return View(licencia);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar la licencia!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Licencia/Delete/5
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }

            return View(licencia);
        }

        // POST: Departamento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Licencias")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguo = db.Licencia.Where(x => x.IdLicencia == id).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var licencia = db.Licencia.Where(x => x.IdLicencia == id).FirstOrDefault();
                    //db.Licencia.Remove(licencia);
                    db.Entry(licencia).State = EntityState.Deleted;
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Licencia", licencia.ValorAntiguo(ValorAntiguo), "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Licencia eliminada exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar licencia!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar licencia!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}
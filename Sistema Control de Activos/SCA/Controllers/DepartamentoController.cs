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
    public class DepartamentoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Departamento
        public ActionResult Index()
        {
            var Model = db.Departamento.ToList();
            return View(Model);
        }

        // GET: Departamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDepartamento, Nombre")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Departamento.Add(departamento);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Departamento", "", departamento.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Departamento Agregado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al agregar departamento!!');</script>";
                            return View(departamento);
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al agregar departamento!!');</script>";
                return View(departamento);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al agregar departamento!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            //ViewBag.IdDepartamento = new SelectList(db.Departamento, "IdDepartamento", "Nombre", departamento.IdDepartamento);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDepartamento, Nombre")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguoEntidad = db.Departamento.Where(x => x.IdDepartamento == departamento.IdDepartamento).FirstOrDefault();
                    string ValorAntiguo = "IdDepartamento:" + ValorAntiguoEntidad.IdDepartamento + " Nombre:" + ValorAntiguoEntidad.Nombre;
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        db.Entry(departamento).State = EntityState.Modified;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Departamento", ValorAntiguo, departamento.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Departamento editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al editar departamento!!');</script>";
                            return View(departamento);
                        }
                    }

                }
                TempData["msg"] = "<script>alert('Error al editar departamento!!');</script>";
                return View(departamento);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar departamento!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Departamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguoEntidad = db.Departamento.Where(x => x.IdDepartamento == id).FirstOrDefault();
                string ValorAntiguo = "IdDepartamento:" + ValorAntiguoEntidad.IdDepartamento + " Nombre:" + ValorAntiguoEntidad.Nombre;
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Departamento departamento = db.Departamento.Where(x => x.IdDepartamento == id).FirstOrDefault();
                    db.Departamento.Remove(departamento);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Departamento", ValorAntiguo, "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Departamento eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar departamento!!');</script>";
                        return View(departamento);
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar departamento!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}
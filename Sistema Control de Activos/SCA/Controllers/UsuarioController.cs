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
    public class UsuarioController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();

        // GET: Usuario
        [AuthorizeUser(idmodulo: "Usuario")]
        public ActionResult Index()
        {
            var user = db.Usuario.Include(a => a.Personal);
           // var use = db.Usuario.Include(a => a.Perfiles);
            return View(user.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        [AuthorizeUserPermises(accion: "A", idmodulo: "Usuario")]
        public ActionResult Create()
        {
            ViewBag.IdPersonallist = db.Personal.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + d.Apellido1 + d.Apellido2,
                    Value = d.IdPersonal.ToString(),
                    Selected = false
                };
            });
            ViewBag.IdPerfillist = db.Perfiles_Acceso.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombrePerfil,
                    Value = d.Id_Perfil.ToString(),
                    Selected = false
                };
            });
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUserPermises(accion: "A", idmodulo: "Usuario")]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        usuario.Contraseña = Helpers.Helper.EncodePassword(string.Concat(usuario.Usuario1.ToString(), usuario.Contraseña.ToString()));
                       
                        db.Usuario.Add(usuario);
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            //var RegistroBitacora = Helpers.Helper.RegistrarMovimiento("Agrego", "Usuarios", "", usuario.IdPerfiles.ToString() + usuario.IdPersonal.ToString(), 1);
                        }
                        else
                        {
                            Ts.Dispose();
                        }
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula");
                //ViewBag.IdPerfiles = new SelectList(db.Perfiles, "IdPerfiles", "Nombre");
                return View(usuario);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        [AuthorizeUserPermises(accion: "E", idmodulo: "Usuario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula");
            //ViewBag.IdPerfiles = new SelectList(db.Perfiles, "IdPerfiles", "Nombre");
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUserPermises(accion: "E", idmodulo: "Usuario")]
        public ActionResult Edit([Bind(Include = "IdUsuario, IdPersonal, IdPerfiles" +
                                                   "Usuario, Password")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdPersonal = new SelectList(db.Personal, "IdPersonal", "Cedula");
                //ViewBag.IdPerfiles = new SelectList(db.Perfiles, "IdPerfiles", "Nombre");
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        [AuthorizeUserPermises(accion: "D", idmodulo: "Usuario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Usuario usuario = db.Usuario.Find(id);
                db.Usuario.Remove(usuario);
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
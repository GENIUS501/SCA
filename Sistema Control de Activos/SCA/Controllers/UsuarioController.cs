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
        public ActionResult Index(string Nombre, string IdDepartamento,string IdPerfiles)
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
            ViewBag.IdPerfillist = db.Perfiles_Acceso.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombrePerfil,
                    Value = d.Id_Perfil.ToString(),
                    Selected = false
                };
            });
            var user = db.Usuario.Include(a => a.Personal).Include(a => a.Perfiles_Acceso).ToList();
            if (Nombre != null)
            {
                user = user.Where(x => x.Personal.Nombre.Contains(Nombre)).ToList();
            }
            if (IdDepartamento != null)
            {
                int id = int.Parse(IdDepartamento);
                user = user.Where(x => x.Personal.IdDepartamento == id).ToList();
            }
            if (IdPerfiles != null)
            {
                int id = int.Parse(IdPerfiles);
                user = user.Where(x => x.IdPerfiles == id).ToList();
            }
            return View(user);
        }

        // GET: Usuario/Details/5
        [AuthorizeUser(idmodulo: "Usuario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarios = db.Usuario.Include(a => a.Personal).Include(a => a.Perfiles_Acceso).ToList();
            var usuario = usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        // [AuthorizeUserPermises(accion: "A", idmodulo: "Usuario")]
        [AuthorizeUser(idmodulo: "Usuario")]
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
        //[AuthorizeUserPermises(accion: "A", idmodulo: "Usuario")]
        [AuthorizeUser(idmodulo: "Usuario")]
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
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Agrego", "Usuario", "", usuario.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Usuario Agregado exitosamente!!');</script>";
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
                            TempData["msg"] = "<script>alert('Error al agregar usuario!!');</script>";
                            return View(usuario);
                        }
                    }
                }
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
                TempData["msg"] = "<script>alert('Error al agregar usuario!!');</script>";
                return View(usuario);
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Error al agregar usuario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Usuario/Edit/5
        // [AuthorizeUserPermises(accion: "E", idmodulo: "Usuario")]
        [AuthorizeUser(idmodulo: "Usuario")]
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
            //ViewBag.IdPerfiles = new SelectList(db.Perfiles, "IdPerfiles", "Nombre");
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [AuthorizeUserPermises(accion: "E", idmodulo: "Usuario")]
        [AuthorizeUser(idmodulo: "Usuario")]
        public ActionResult Edit(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguoEntidad = db.Usuario.Where(x => x.IdUsuario == usuario.IdUsuario).FirstOrDefault();
                    string ValorAntiguo = "Contraseña:" + ValorAntiguoEntidad.Contraseña.ToString() + " IdPerfiles:" + ValorAntiguoEntidad.IdPerfiles + " IdPersonal:" + ValorAntiguoEntidad.IdPersonal + " IdUsuario:" + ValorAntiguoEntidad.IdUsuario + " Usuario1:" + ValorAntiguoEntidad.Usuario1;
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var UsuarioGuardar = db.Usuario.Where(x => x.IdUsuario == usuario.IdUsuario).FirstOrDefault();
                        if (UsuarioGuardar.Contraseña != usuario.Contraseña)
                        {
                            UsuarioGuardar.Contraseña = Helpers.Helper.EncodePassword(string.Concat(usuario.Usuario1.ToString(), usuario.Contraseña.ToString()));
                        }
                        UsuarioGuardar.IdPerfiles = usuario.IdPerfiles;
                        UsuarioGuardar.IdPersonal = usuario.IdPersonal;
                        UsuarioGuardar.IdUsuario = usuario.IdUsuario;
                        UsuarioGuardar.Usuario1 = usuario.Usuario1;
                        //db.Entry(usuario).State = EntityState.Modified;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Editado", "Usuario", ValorAntiguo, usuario.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Usuario editado exitosamente!!');</script>";
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
                            TempData["msg"] = "<script>alert('Error al agregar usuario!!');</script>";
                            return View(usuario);
                        }
                    }
                }
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
                //ViewBag.IdPerfiles = new SelectList(db.Perfiles, "IdPerfiles", "Nombre");
                return View(usuario);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar el usuario!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Usuario/Delete/5
        //[AuthorizeUserPermises(accion: "D", idmodulo: "Usuario")]
        [AuthorizeUser(idmodulo: "Usuario")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarios = db.Usuario.Include(a => a.Personal).Include(a => a.Perfiles_Acceso).ToList();
            var usuario = usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: ControlInventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Usuario")]
        public ActionResult Delete(int id)
        {
            try
            {
                var ValorAntiguoEntidad = db.Usuario.Where(x => x.IdUsuario == id).FirstOrDefault();
                string ValorAntiguo = "Contraseña:" + ValorAntiguoEntidad.Contraseña.ToString() + " IdPerfiles:" + ValorAntiguoEntidad.IdPerfiles + " IdPersonal:" + ValorAntiguoEntidad.IdPersonal + " IdUsuario:" + ValorAntiguoEntidad.IdUsuario + " Usuario1:" + ValorAntiguoEntidad.Usuario1;
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Usuario usuario = db.Usuario.Find(id);
                    db.Usuario.Remove(usuario);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Usuario", ValorAntiguo, "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('Usuario eliminado exitosamente!!');</script>";
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
                        TempData["msg"] = "<script>alert('Error al eliminar usuario!!');</script>";
                        return View(usuario);
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar usuario!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}
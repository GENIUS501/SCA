using Filters;
using SCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SCA.Controllers
{
    public class Perfiles_AccesoController : Controller
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        // GET: Perfiles_Acceso
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Index()
        {
            var Modelo = db.Perfiles_Acceso.ToList();
            return View(Modelo);
        }

        // GET: Perfiles_Acceso/Details/5
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Perfiles_Acceso/Create
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Create()
        {
            PerfilesViewModel Modelo = new PerfilesViewModel();
            Modelo.ModulosEscogidos = new List<PerfilesViewModel.Modulos>();
            Modelo.ModulosEscogidos.Add(new PerfilesViewModel.Modulos
            {
                Modulo = "Prueba",
                Checked = "s"
            });
            return View(Modelo);
        }

        // POST: Perfiles_Acceso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Create(PerfilesViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var Permisos = Grabar(Modelo.ModulosEscogidos, Modelo.Id_Perfil);
                            if (Permisos.Count > 0)
                            {
                                var Permisos_Anteriores = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Modelo.Id_Perfil).ToList();
                                if (Permisos_Anteriores.Count > 0)
                                {
                                    db.Perfiles_Permisos.RemoveRange(Permisos_Anteriores);
                                }
                                db.Perfiles_Permisos.AddRange(Permisos);
                            }
                            Perfiles_Acceso Perfil = new Perfiles_Acceso();
                            Perfil.NombrePerfil = Modelo.NombrePerfil;
                            Perfil.Descripcion = Modelo.Descripcion;
                            db.Perfiles_Acceso.Add(Perfil);
                            int Resultado = db.SaveChanges();
                            if (Resultado > 0)
                            {
                                Ts.Complete();
                                var UsuarioLogueado = (Usuario)Session["User"];
                                Helpers.Helper.RegistrarMovimiento("Agrego", "Perfiles", "", Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                                TempData["msg"] = "<script>alert('Perfil Agregado exitosamente!!');</script>";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                Ts.Dispose();
                                TempData["msg"] = "<script>alert('Error al agregar el perfil!!');</script>";
                                return View(Modelo);
                            }
                        }
                    }
                }
                TempData["msg"] = "<script>alert('Error al agregar el perfil!!');</script>";
                return View(Modelo);
            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('Error al agregar el perfil!!');</script>";
                return RedirectToAction("Index");
            }
        }
        private List<Perfiles_Permisos> Grabar(List<PerfilesViewModel.Modulos> Permisos, int IdPerfil)
        {
            try
            {
                List<Perfiles_Permisos> PermisosGuardar = new List<Perfiles_Permisos>();
                foreach (var Item in Permisos)
                {
                    if (Item.Checked == "true")
                    {
                        PermisosGuardar.Add(new Perfiles_Permisos
                        {
                            Id_Perfil = IdPerfil,
                            Modulo = Item.Modulo,
                            Agregar = "s",
                            Eliminar = "s",
                            Modificar = "s"
                        });
                    }
                }
                return PermisosGuardar;
            }
            catch (Exception)
            {
                return new List<Perfiles_Permisos>();
            }
        }
        // GET: Perfiles_Acceso/Edit/5
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Edit(int Id)
        {
            try
            {
                var Perfil = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Id).FirstOrDefault();
                var PerfilPermiso = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Id).ToList();
                PerfilesViewModel Modelo = new PerfilesViewModel();
                Modelo.Id_Perfil = Perfil.Id_Perfil;
                Modelo.Descripcion = Perfil.Descripcion;
                Modelo.NombrePerfil = Perfil.NombrePerfil;
                Modelo.ModulosEscogidos = new List<PerfilesViewModel.Modulos>();
                foreach (var Item in PerfilPermiso)
                {
                    Modelo.ModulosEscogidos.Add(new PerfilesViewModel.Modulos
                    {
                        Modulo = Item.Modulo,
                        Checked = "true",
                    }
                    );
                }
                return View(Modelo);
            }
            catch (Exception)
            {
                return View();
            }

        }

        // POST: Perfiles_Acceso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Edit(PerfilesViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ValorAntiguo = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Modelo.Id_Perfil).FirstOrDefault();
                    using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var Permisos = Grabar(Modelo.ModulosEscogidos, Modelo.Id_Perfil);
                        if (Permisos.Count > 0)
                        {
                            var Permisos_Anteriores = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Modelo.Id_Perfil).ToList();
                            if (Permisos_Anteriores.Count > 0)
                            {
                                db.Perfiles_Permisos.RemoveRange(Permisos_Anteriores);
                            }
                            db.Perfiles_Permisos.AddRange(Permisos);
                        }
                        var Objbd = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Modelo.Id_Perfil).FirstOrDefault();
                        Objbd.Id_Perfil = Modelo.Id_Perfil;
                        Objbd.NombrePerfil = Modelo.NombrePerfil;
                        Objbd.Descripcion = Modelo.Descripcion;
                        int Resultado = db.SaveChanges();
                        if (Resultado > 0)
                        {
                            Ts.Complete();
                            var UsuarioLogueado = (Usuario)Session["User"];
                            Helpers.Helper.RegistrarMovimiento("Edito", "Perfiles", Modelo.ValorAntiguo(ValorAntiguo), Modelo.ValorNuevo(), UsuarioLogueado.IdUsuario);
                            TempData["msg"] = "<script>alert('Perfil editado exitosamente!!');</script>";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Ts.Dispose();
                            TempData["msg"] = "<script>alert('Error al editar el perfil!!');</script>";
                            return View(Modelo);
                        }
                    }
                }
                return View(Modelo);
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al editar el perfil!!');</script>";
                return RedirectToAction("Index");
            }
        }

        // GET: Perfiles_Acceso/Delete/5
        [HttpGet]
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Delete(int Id)
        {
            var Perfil = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Id).FirstOrDefault();
            var PerfilPermiso = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Perfil.Id_Perfil).ToList();
            PerfilesViewModel Modelo = new PerfilesViewModel();
            Modelo.Id_Perfil = Perfil.Id_Perfil;
            Modelo.Descripcion = Perfil.Descripcion;
            Modelo.NombrePerfil = Perfil.NombrePerfil;
            Modelo.ModulosEscogidos = new List<PerfilesViewModel.Modulos>();
            foreach (var Item in PerfilPermiso)
            {
                Modelo.ModulosEscogidos.Add(new PerfilesViewModel.Modulos
                {
                    Modulo = Item.Modulo,
                    Checked = "true",
                }
                );
            }
            return View(Modelo);
        }

        // POST: Perfiles_Acceso/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idmodulo: "Perfiles")]
        public ActionResult Delete(string Id)
        {
            try
            {
                int Ida = int.Parse(Id);
                var ValorAntiguo = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Ida).FirstOrDefault();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var Permisos_Anteriores = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Ida).ToList();
                    if (Permisos_Anteriores.Count > 0)
                    {
                        db.Perfiles_Permisos.RemoveRange(Permisos_Anteriores);
                    }
                    var Objbd = db.Perfiles_Acceso.Where(x => x.Id_Perfil == Ida).FirstOrDefault();
                    db.Perfiles_Acceso.Remove(Objbd);
                    int Resultado = db.SaveChanges();
                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        var UsuarioLogueado = (Usuario)Session["User"];
                        PerfilesViewModel Modelo = new PerfilesViewModel();
                        Helpers.Helper.RegistrarMovimiento("Elimino", "Perfil", Modelo.ValorAntiguo(ValorAntiguo), "", UsuarioLogueado.IdUsuario);
                        TempData["msg"] = "<script>alert('perfil eliminado exitosamente!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Ts.Dispose();
                        TempData["msg"] = "<script>alert('Error al eliminar el perfil!!');</script>";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Error al eliminar el perfil!!');</script>";
                return RedirectToAction("Index");
            }
        }
    }
}

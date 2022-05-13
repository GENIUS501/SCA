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
        public ActionResult Index()
        {
            var Modelo = db.Perfiles_Acceso.ToList();
            return View(Modelo);
        }

        // GET: Perfiles_Acceso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Perfiles_Acceso/Create
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
        public ActionResult Create(PerfilesViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            Perfiles_Acceso Perfil = new Perfiles_Acceso();
                            Perfil.NombrePerfil = Modelo.NombrePerfil;
                            Perfil.Descripcion = Modelo.Descripcion;
                            db.Perfiles_Acceso.Add(Perfil);
                            int Resultado = db.SaveChanges();
                            if (Resultado > 0)
                            {
                                var Permisos = Grabar(Modelo.ModulosEscogidos, Perfil.Id_Perfil);
                                if (Permisos.Count > 0)
                                {
                                    var Permisos_Anteriores = db.Perfiles_Permisos.Where(x => x.Id_Perfil == Perfil.Id_Perfil).ToList();
                                    if (Permisos_Anteriores.Count > 0)
                                    {
                                        db.Perfiles_Permisos.RemoveRange(Permisos_Anteriores);
                                    }
                                    db.Perfiles_Permisos.AddRange(Permisos);
                                }
                                db.SaveChanges();
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
            catch(Exception ex)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Perfiles_Acceso/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfiles_Acceso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfiles_Acceso/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

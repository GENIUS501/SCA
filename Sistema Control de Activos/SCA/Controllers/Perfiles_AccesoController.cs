using SCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Create(PerfilesViewModel Model)
        {
            try
            {
                
                // TODO: Add insert logic here

                return View();
            }
            catch
            {
                return View();
            }
        }
        private bool Grabar()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return true;
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

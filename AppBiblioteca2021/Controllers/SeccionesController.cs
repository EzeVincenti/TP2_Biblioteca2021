using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppBiblioteca2021.Data;
using AppBiblioteca2021.Models;

namespace AppBiblioteca2021.Controllers
{
    public class SeccionesController : Controller
    {
        private AppBiblioteca2021Context db = new AppBiblioteca2021Context();

        // GET: Secciones
        public ActionResult Index(string MensajeDevuelto)
        {
            ViewBag.MensajeDevuelto = MensajeDevuelto;
            return View(db.Secciones.ToList());
        }

        // GET: Secciones/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secciones secciones = db.Secciones.Find(id);
        //    if (secciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(secciones);
        //}

        // GET: Secciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeccionesID,SeccionesNombre")] Secciones secciones)
        {
            if (ModelState.IsValid)
            {
                db.Secciones.Add(secciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secciones);
        }

        // GET: Secciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = db.Secciones.Find(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }

        // POST: Secciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeccionesID,SeccionesNombre")] Secciones secciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secciones);
        }

        // GET: Secciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secciones secciones = db.Secciones.Find(id);
        //    if (secciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(secciones);
        //}

        //// POST: Secciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //VALIDACIÓN PARA QUE NO SE ELIMINE SI ESTA RELACIONADO
            
            var mensajeDevuelto = "";

            var libros = (from a in db.Libros where a.SeccionesID == id select a).Count();
            if (libros > 0)
            {
                mensajeDevuelto = "No se puede eliminar la Sección seleccionada porque está relacionado con un Libro.";
            }
            else
            {
                Secciones secciones = db.Secciones.Find(id);
                db.Secciones.Remove(secciones);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { MensajeDevuelto = mensajeDevuelto });
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

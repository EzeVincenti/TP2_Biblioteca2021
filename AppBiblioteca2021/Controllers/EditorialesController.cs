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
    public class EditorialesController : Controller
    {
        private AppBiblioteca2021Context db = new AppBiblioteca2021Context();

        // GET: Editoriales
        public ActionResult Index(string MensajeDevuelto)
        {
            ViewBag.MensajeDevuelto = MensajeDevuelto;
            return View(db.Editoriales.ToList());
        }

        // GET: Editoriales/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Editoriales editoriales = db.Editoriales.Find(id);
        //    if (editoriales == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editoriales);
        //}

        //// GET: Editoriales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditorialesID,EditorialesNombre")] Editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                db.Editoriales.Add(editoriales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editoriales);
        }

        // GET: Editoriales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editoriales editoriales = db.Editoriales.Find(id);
            if (editoriales == null)
            {
                return HttpNotFound();
            }
            return View(editoriales);
        }

        // POST: Editoriales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditorialesID,EditorialesNombre")] Editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editoriales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editoriales);
        }

        // GET: Editoriales/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Editoriales editoriales = db.Editoriales.Find(id);
        //    if (editoriales == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editoriales);
        //}

        //// POST: Editoriales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            //VALIDACIÓN PARA QUE NO SE ELIMINE SI ESTA RELACIONADO

            var mensajeDevuelto = "";

            var libros = (from a in db.Libros where a.EditorialesID == id select a).Count();
            if (libros > 0)
            {
                mensajeDevuelto = "No se puede eliminar la Editorial porque está relacionada con un Libro.";
            }
            else
            {
                Editoriales editoriales = db.Editoriales.Find(id);
                db.Editoriales.Remove(editoriales);
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

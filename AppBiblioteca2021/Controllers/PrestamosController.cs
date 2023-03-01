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
    public class PrestamosController : Controller
    {
        private AppBiblioteca2021Context db = new AppBiblioteca2021Context();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Socios);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto");

            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");


            return View();
        }

        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrestamosID,PrestamosFecha,PrestamosFechaDevolucion,SociosID")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {

                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Prestamos.Add(prestamos);
                        db.SaveChanges();

                        var prestamosDetallesT = (from a in db.PrestamosDetallesT select a).ToList();
                        foreach (var item in prestamosDetallesT)
                        {
                            var libroGuardar = new PrestamosDetalles
                            {
                                LibrosID = item.LibrosID,
                                PrestamosID = prestamos.PrestamosID
                            };
                            db.PrestamosDetalles.Add(libroGuardar);
                            db.SaveChanges();
                        }
                        db.PrestamosDetallesT.RemoveRange(prestamosDetallesT);
                        db.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                    }
                }

            }

            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);

            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");

            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);
        //    ViewBag.LibrosID = new SelectList(db.Libros, "LibrosID", "LibrosTitulo");

        //    return View(prestamos);
        //}

        //// POST: Prestamos/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        //// más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "PrestamosID,PrestamosFecha,PrestamosFechaDevolucion,SociosID")] Prestamos prestamos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(prestamos).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);
        //    ViewBag.LibrosID = new SelectList(db.Libros, "LibrosID", "LibrosTitulo");

        //    return View(prestamos);
        //}

        // GET: Prestamos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(prestamos);
        //}

        // POST: Prestamos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //RELACION CON LA TABLA TEMPORAL Y PRUEBA DE CORRECTO GUARDADO SI NO DA ERROR--------------------------------------------
        public JsonResult GuardarLibro(int LibrosID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    //CAMBIA EL ESTADO DEL LIBRO--------------------------------------------

                    var libros = (from a in db.Libros where a.LibrosID == LibrosID select a).SingleOrDefault();
                    libros.EstadoLibros = EstadoLibros.Prestado;
                    db.SaveChanges();

                    var libroGuardar = new PrestamosDetallesT
                    {
                        LibrosID = libros.LibrosID,
                        LibrosTitulo = libros.LibrosTitulo
                    };
                    db.PrestamosDetallesT.Add(libroGuardar);
                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            //PARA QUE NO APAREZCA DE NUEVO EL LIBRO QUE YA AGREGUE EN EL COMBO. SOLO LOS DISPONIBLES.----------------------

            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //CREA EL LISTADO PARA MOSTRAR EN LA GRILLA DE LA VISTA --------------------------------------------------------------------

        public JsonResult BuscarLibros()
        {
            List<PrestamosDetallesT> listadoPrestamosDetallesT = new List<PrestamosDetallesT>();

            var librosTemporal = (from a in db.PrestamosDetallesT select a).ToList();

            foreach (var item in librosTemporal)
            {
                var libroBuscar = new PrestamosDetallesT
                {
                    PrestamosDetallesTID = item.PrestamosDetallesTID,
                    LibrosID = item.LibrosID,
                    LibrosTitulo = item.LibrosTitulo
                };
                listadoPrestamosDetallesT.Add(libroBuscar);                
            }

            return Json(listadoPrestamosDetallesT, JsonRequestBehavior.AllowGet);
        }


        //CANCELAR PRESTAMO ---------------------------------------------------------------------------------------------------------------
        public JsonResult CancelarPrestamo()
        {
            var resultado = true;

            var prestamosDetallesT = (from a in db.PrestamosDetallesT select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in prestamosDetallesT)
                    {
                        var libro = (from a in db.Libros where a.LibrosID == item.LibrosID select a).SingleOrDefault();
                        libro.EstadoLibros = EstadoLibros.Disponible;
                        db.SaveChanges();
                    }

                    db.PrestamosDetallesT.RemoveRange(prestamosDetallesT);
                    db.SaveChanges();

                   transaccion.Commit();

                   resultado = true;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }



        //ELIMINAR LIBRO DE LA TABLA------------------------------------------------------------------------------------------
        public JsonResult EliminarLibro(int PrestamosDetallesTID)
        {
            var resultado = true;

            //var prestamosDetallesT = (from a in db.PrestamosDetallesT select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    PrestamosDetallesT prestamoDetallesT = db.PrestamosDetallesT.Find(PrestamosDetallesTID);
                    db.PrestamosDetallesT.Remove(prestamoDetallesT);

                    var libro = (from a in db.Libros where a.LibrosID == prestamoDetallesT.LibrosID select a).SingleOrDefault();
                    libro.EstadoLibros = EstadoLibros.Disponible;

                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
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

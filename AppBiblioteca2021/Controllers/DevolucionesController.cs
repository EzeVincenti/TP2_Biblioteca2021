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
    public class DevolucionesController : Controller
    {
        private AppBiblioteca2021Context db = new AppBiblioteca2021Context();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devoluciones = db.Devoluciones.Include(d => d.Socios);
            return View(devoluciones.ToList());
        }

        // GET: Devoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            if (devoluciones == null)
            {
                return HttpNotFound();
            }
            return View(devoluciones);
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto");
            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Prestado select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");

            return View();
        }

        // POST: Devoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DevolucionesID,DevolucionesFecha,SociosID")] Devoluciones devoluciones)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Devoluciones.Add(devoluciones);
                        db.SaveChanges();

                        var devolucionesDetallesT = (from a in db.DevolucionesDetallesT select a).ToList();
                        foreach (var item in devolucionesDetallesT)
                        {
                            var libroGuardar = new DevolucionesDetalles
                            {
                                LibrosID = item.LibrosID,
                                DevolucionesID = devoluciones.DevolucionesID,
                            };
                            db.DevolucionesDetalles.Add(libroGuardar);
                            db.SaveChanges();
                        }

                        db.DevolucionesDetallesT.RemoveRange(devolucionesDetallesT);
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

            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", devoluciones.SociosID);
           
            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Prestado select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");
            return View(devoluciones);
        }

        //// GET: Devoluciones/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devoluciones devoluciones = db.Devoluciones.Find(id);
        //    if (devoluciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", devoluciones.SociosID);
        //    return View(devoluciones);
        //}

        // POST: Devoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DevolucionesID,DevolucionesFecha,SociosID")] Devoluciones devoluciones)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(devoluciones).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", devoluciones.SociosID);
        //    return View(devoluciones);
        //}

        //// GET: Devoluciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Devoluciones devoluciones = db.Devoluciones.Find(id);
        //    if (devoluciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(devoluciones);
        //}

        //// POST: Devoluciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devoluciones devoluciones = db.Devoluciones.Find(id);
            db.Devoluciones.Remove(devoluciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //RELACION CON LA TABLA TEMPORAL Y PRUEBA DE CORRECTO GUARDADO SI NO DA ERROR--------------------------------------------
        public JsonResult DevolverLibro(int LibrosID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    //CAMBIA EL ESTADO DEL LIBRO--------------------------------------------

                    var libros = (from a in db.Libros where a.LibrosID == LibrosID select a).SingleOrDefault();
                    libros.EstadoLibros = EstadoLibros.Disponible;
                    db.SaveChanges();

                    var libroDevolver = new DevolucionesDetallesT
                    {
                        LibrosID = libros.LibrosID,
                        LibrosTitulo = libros.LibrosTitulo
                    };
                    db.DevolucionesDetallesT.Add(libroDevolver);
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

            //PARA QUE APAREZCAN SOLO LOS QUE ESTAN PRESTADOS.--------------------------------------------

            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Prestado select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibrosTitulo");


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }


        //CREA EL LISTADO PARA MOSTRAR EN LA GRILLA DE LA VISTA --------------------------------------------------------------------

        public JsonResult BuscarLibros()
        {
            List<DevolucionesDetallesT> listadoDevolucionesDetallesT = new List<DevolucionesDetallesT>();

            var librosTemporal = (from a in db.DevolucionesDetallesT select a).ToList();

            foreach (var item in librosTemporal)
            {
                var libroBuscar = new DevolucionesDetallesT
                {
                    DevolucionesDetallesTID = item.DevolucionesDetallesTID,
                    LibrosID = item.LibrosID,
                    LibrosTitulo = item.LibrosTitulo
                };
                listadoDevolucionesDetallesT.Add(libroBuscar);
            }

            return Json(listadoDevolucionesDetallesT, JsonRequestBehavior.AllowGet);
        }


        //CANCELAR DEVOLUCIÓN ---------------------------------------------------------------------------------------------------------------
        public JsonResult CancelarDevolucion()
        {
            var resultado = true;

            var devolucionesDetallesT = (from a in db.DevolucionesDetallesT select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in devolucionesDetallesT)
                    {
                        var libro = (from a in db.Libros where a.LibrosID == item.LibrosID select a).SingleOrDefault();
                        libro.EstadoLibros = EstadoLibros.Prestado;
                        db.SaveChanges();
                    }

                    db.DevolucionesDetallesT.RemoveRange(devolucionesDetallesT);
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
        public JsonResult EliminarLibro(int DevolucionesDetallesTID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    DevolucionesDetallesT devolucionesDetallesT = db.DevolucionesDetallesT.Find(DevolucionesDetallesTID);
                    db.DevolucionesDetallesT.Remove(devolucionesDetallesT);

                    var libro = (from a in db.Libros where a.LibrosID == devolucionesDetallesT.LibrosID select a).SingleOrDefault();
                    libro.EstadoLibros = EstadoLibros.Prestado;

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

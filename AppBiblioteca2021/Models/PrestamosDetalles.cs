using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class PrestamosDetalles
    {
        [Key]
        public int PrestamosDetallesID { get; set; }


        public int PrestamosID { get; set; }
        public virtual Prestamos Prestamos { get; set; }


        [Display(Name = "Libro")]
        public int LibrosID { get; set; }
        public virtual Libros Libros { get; set; }




    }
}
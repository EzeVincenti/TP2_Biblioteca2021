using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class DevolucionesDetalles
    {
        [Key]
        public int DevolucionesDetallesID { get; set; }

        public int DevolucionesID { get; set; }
        public virtual Devoluciones Devoluciones { get; set; }


        [Display(Name = "Libro")]
        public int LibrosID { get; set; }
        public virtual Libros Libros { get; set; }

    }
}
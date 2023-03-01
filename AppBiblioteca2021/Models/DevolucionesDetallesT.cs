using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class DevolucionesDetallesT
    {
        [Key]
        public int DevolucionesDetallesTID { get; set; }

        public int LibrosID { get; set; }

        [Display(Name = "Título")]
        public string LibrosTitulo { get; set; }

    }
}
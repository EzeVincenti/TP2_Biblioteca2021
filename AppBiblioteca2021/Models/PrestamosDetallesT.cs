using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class PrestamosDetallesT
    {
        [Key]
        public int PrestamosDetallesTID { get; set; }

        public int LibrosID { get; set; }
        
        [Display(Name = "Título")]
        public string LibrosTitulo { get; set; }

    }
}
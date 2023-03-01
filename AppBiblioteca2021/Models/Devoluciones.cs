using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Devoluciones
    {
        [Key]
        public int DevolucionesID { get; set; }


        [Display(Name = "Fecha devolución")]
        [DataType(DataType.Date)]
        public DateTime DevolucionesFecha { get; set; }

        [Display(Name = "Socio")]
        public int SociosID { get; set; }
        public virtual Socios Socios { get; set; }


        public virtual ICollection<DevolucionesDetalles> DevolucionesDetalles { get; set; }

    }
}
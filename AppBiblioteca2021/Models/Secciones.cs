using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Secciones
    {
        [Key]
        public int SeccionesID { get; set; }

        [Display(Name = "Sección")]
        [Required(ErrorMessage = "El nombre de la sección es obligatorio")]
        [StringLength(50,ErrorMessage = "La sección no debe superar los 50 caracteres")]
        public string SeccionesNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
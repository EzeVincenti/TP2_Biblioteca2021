using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Autores
    {
        [Key]
        public int AutoresID { get; set; }

        [Display(Name = "Nombre del Autor")]
        [Required(ErrorMessage = "El nombre del autor es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre del autor no debe superar los 50 caracteres")]
        public string AutoresNombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido del autor es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido del autor no debe superar los 50 caracteres")]
        public string AutoresApellido { get; set; }

        [NotMapped]
        [Display(Name = "Nombre completo")]
        public string AutoresNombreCompleto 
        { get {
                return string.Format("{0} {1}", AutoresNombre, AutoresApellido); 
            }
        }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
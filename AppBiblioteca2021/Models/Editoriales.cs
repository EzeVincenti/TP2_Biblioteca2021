using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Editoriales
    {
        [Key]
        public int EditorialesID { get; set; }

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "El nombre de la editorial es obligatorio")]
        [StringLength(50, ErrorMessage = "La editorial no debe superar los 50 caracteres")]
        public string EditorialesNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
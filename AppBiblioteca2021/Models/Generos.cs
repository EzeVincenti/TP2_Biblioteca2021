using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Generos
    {
        [Key]
        public int GenerosID { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "El nombre del género es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no debe superar los 50 caracteres")]
        public string GenerosNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Libros
    {
       [Key]
        public int LibrosID { get; set; }

        [Display(Name = "N° ISBN")]
        [StringLength(20, ErrorMessage = "El número no debe superar los 20 caracteres")]
        public string LibrosISBN { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(250, ErrorMessage = "El título no debe superar los 250 caracteres")]
        public string LibrosTitulo { get; set; }

        [Display(Name = "Reseña")]
        [StringLength(400, ErrorMessage = "El reseña no debe superar los 400 caracteres")]
        public string LibrosResenia { get; set; }

        [Display(Name = "Publicación")]
        [DataType(DataType.Date)]
        public DateTime LibrosFechaPublicacion { get; set; }

        [Display(Name = "Estado")]
        public EstadoLibros EstadoLibros { get; set; }

        
        [Display(Name = "Autor")]
        public int AutoresID { get; set; }
        public virtual Autores Autores { get; set; }


        [Display(Name = "Editorial")]
        public int EditorialesID { get; set; }
        public virtual Editoriales Editoriales{ get; set; }

        [Display(Name = "Género")]
        public int GenerosID { get; set; }
        public virtual Generos Generos { get; set; }

        [Display(Name = "Sección")]
        public int SeccionesID { get; set; }
        public virtual Secciones Secciones { get; set; }

        public virtual ICollection<PrestamosDetalles> PrestamosDetalles { get; set; }
        public virtual ICollection<DevolucionesDetalles> DevolucionesDetalles { get; set; }

    }

    public enum EstadoLibros { 
    Disponible,
    Prestado
    }
}
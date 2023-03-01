using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Models
{
    public class Socios
    {
        [Key]
        public int SociosID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no debe superar los 50 caracteres")]
        public string SociosNombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no debe superar los 50 caracteres")]
        public string SociosApellido { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La dirección es obligatorio")]
        [StringLength(50, ErrorMessage = "La dirección no debe superar los 50 caracteres")]
        public string SociosDireccion { get; set; }

        [Display(Name = "Teléfono")]
        public string SociosTelefono { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime SociosFechaNacimiento { get; set; }


        [NotMapped]
        [Display(Name = "Nombre completo")]
        public string SociosNombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", SociosNombre, SociosApellido);
            }
        }

        [NotMapped]
        [Display(Name = "Edad")]
        public int Edad
        {
            get
            {
                return DateTime.Now.Year - SociosFechaNacimiento.Year;
            }
        }
        public virtual ICollection<Prestamos> Prestamos{ get; set; }

        public virtual ICollection<Devoluciones> Devoluciones { get; set; }

    }
}
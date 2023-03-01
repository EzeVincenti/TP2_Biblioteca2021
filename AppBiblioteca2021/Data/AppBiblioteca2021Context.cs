using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppBiblioteca2021.Data
{
    public class AppBiblioteca2021Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AppBiblioteca2021Context() : base("name=AppBiblioteca2021Context")
        {
        }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Autores> Autores { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Secciones> Secciones { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Editoriales> Editoriales { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Generos> Generos { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Socios> Socios { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Libros> Libros { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Prestamos> Prestamos { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.PrestamosDetalles> PrestamosDetalles { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.PrestamosDetallesT> PrestamosDetallesT { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.Devoluciones> Devoluciones { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.DevolucionesDetalles> DevolucionesDetalles { get; set; }

        public System.Data.Entity.DbSet<AppBiblioteca2021.Models.DevolucionesDetallesT> DevolucionesDetallesT { get; set; }


    }
}

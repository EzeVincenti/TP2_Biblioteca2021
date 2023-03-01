namespace AppBiblioteca2021.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppBiblioteca2021.Data.AppBiblioteca2021Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "AppBiblioteca2021.Data.AppBiblioteca2021Context";

        }

        protected override void Seed(AppBiblioteca2021.Data.AppBiblioteca2021Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

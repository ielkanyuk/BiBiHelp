using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace System.Data.Entity
{
    public static class EntityFrameworkExtensions
    {
        public static IEnumerable<object> AsEnumerable(this DbSet set)
        {
            return Enumerable.Cast<object>(set);
        }
    }
}

namespace BiBiHelp.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Container>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Container context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

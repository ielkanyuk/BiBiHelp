using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiBiHelp.Model.Entities;
using BiBiHelp.Model.Migrations;

namespace BiBiHelp.Model
{
    class Container : DbContext
    {
        public Container() : base("name=Container")
        {
        }

        public Container(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new ContainerInitializer());
            Database.Initialize(true);
        }

        public void Detach(object entity)
        {
            (this as IObjectContextAdapter).ObjectContext.Detach(entity);
        }

        internal class ContainerInitializer : MigrateDatabaseToLatestVersion<Container, Configuration> //DropCreateDatabaseAlways<Container>
        {
            protected void Seed(Container context)
            {
                new DbSeed(context).Seed();
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        // При сохранении изменений проставляем автоматически дату создания и дату изменения сущности
        public override int SaveChanges()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;

            //Find all Entities that are Added/Modified that inherit from my Entity
            var objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    e.Entity is Entity
                select e;

            var currentTime = DateTime.UtcNow;

            foreach (var entry in objectStateEntries)
            {
                var entityBase = entry.Entity as Entity;
                if (entityBase == null) continue;

                if (entry.State == EntityState.Added)
                    entityBase.DateCreated = currentTime;

                entityBase.DateUpdated = currentTime;
            }

            return base.SaveChanges();
        }
    }
}

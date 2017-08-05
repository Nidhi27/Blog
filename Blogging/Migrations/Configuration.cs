namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Blogging.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Blogging.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Blogging.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Tags.AddOrUpdate(
            //  p => p.Id,
            //  new Tag { Id = 1, Name = "Personal", PostId=1 },
            //  new Tag { Id = 2, Name = " Stories" , PostId=1}

            //);

        }
    }
}

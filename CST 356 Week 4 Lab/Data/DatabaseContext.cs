using CST_356_Week_4_Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CST_356_Week_4_Lab.Data
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
    }

    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        // intentionally left blank
    }
}
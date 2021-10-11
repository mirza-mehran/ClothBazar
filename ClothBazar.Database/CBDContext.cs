using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Database
{
    public  class CBDContext : DbContext
    {
        public CBDContext():base("ClothBazarCon")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CBDContext, Migrations.Configuration>());
        }
        public DbSet<Product> Products
        {
            get; set;
        }
        public DbSet<Category> Categories
        {
            get; set;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}

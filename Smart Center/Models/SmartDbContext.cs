using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Smart_Center.Models
{
    class SmartDbContext: Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }  
        public DbSet<Purchas> Purchas { set; get; }
        public DbSet<PurchasDetail> PurchasDetail { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<DebtPay> DebtPays { get; set; }
        public DbSet<IMEI> iMEIs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder connectionString = new System.Data.SqlClient.SqlConnectionStringBuilder() {
                   DataSource = @"DESKTOP-QCB22M1\SQLEXPRESS",
                   InitialCatalog = "Smart",
                   IntegratedSecurity =true
            };

            optionsBuilder.UseSqlServer(connectionString.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //For Admin Uniqe 
            modelBuilder.Entity<Admin>().HasIndex(u => u.Username).IsUnique(true);

            //for barcode UNIQE
            modelBuilder.Entity<Product>().HasIndex(u => u.Barcode).IsUnique(true);

            // PRIMARY KEY FOR DIBTS
            modelBuilder.Entity<Debt>().HasKey(x => new { x.Company_Id,x.Purchas_Id});

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}

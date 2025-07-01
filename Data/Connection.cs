using Data.CigaretteTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection : DbContext
    {
        public DbSet<СigarettesProduct> СigarettesProducts {  get; set; }
        public DbSet<CigarettesPhoto> CigarettesPhotos { get; set; }
        public DbSet<CigarettesCategorie> CigarettesCategories { get; set; }
        public DbSet<CigarettesManufacturer> CigarettesManufacturer { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Connection(DbContextOptions<Connection> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity< СigarettesProduct>().HasQueryFilter(row=>!row.IsDelete);
            modelBuilder.Entity<CigarettesPhoto>().HasQueryFilter(row => !row.IsDelete);
            modelBuilder.Entity<CigarettesCategorie>().HasQueryFilter(row => !row.IsDelete);
            modelBuilder.Entity<CigarettesManufacturer>().HasQueryFilter(row => !row.IsDelete);
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Clients)
                .UsingEntity<Dictionary<string, object>>("ClientRole",
                row => row.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                row => row.HasOne<Client>().WithMany().HasForeignKey("ClientId"),
                row =>
                {
                    row.HasKey("ClientId", "RoleId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}

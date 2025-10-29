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

        //переопределил сохранение в бд для автоматического обновления дат времени создания и добавления
        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Reservation or ReservationItem)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.CurrentValues["CreatedAt"] = now;
                        entry.CurrentValues["UpdatedAt"] = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.CurrentValues["UpdatedAt"] = now;
                    }
                }
            }
            return base.SaveChangesAsync(ct);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //enum для бд
            modelBuilder.Entity<Reservation>()
            .Property(x => x.reservationStatus)
            .HasConversion<string>()      
            .HasMaxLength(20);

            modelBuilder.Entity<ReservationItem>().HasQueryFilter(row => !row.IsDelete);
            modelBuilder.Entity<Reservation>().HasQueryFilter(row => !row.IsDelete);
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

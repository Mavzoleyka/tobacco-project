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
        public DbSet<Product> СigarettesProducts {  get; set; }
        public DbSet<ProductPhoto> CigarettesPhotos { get; set; }
        public DbSet<ProductCategory> CigarettesCategories { get; set; }
        public DbSet<Manufacturer> CigarettesManufacturer { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }
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
            
            modelBuilder.Entity<Reservation>()
                .Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            
            modelBuilder.Entity<Reservation>()
                .Property(x => x.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ReservationItem>()
                .Property(x => x.PriceAtBooking)
                .HasPrecision(10, 2);

            
            modelBuilder.Entity<Reservation>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<ReservationItem>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<ProductPhoto>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<ProductCategory>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Manufacturer>().HasQueryFilter(x => !x.IsDelete);

            
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientRole",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<Client>().WithMany().HasForeignKey("ClientId"),
                    j => { j.HasKey("ClientId", "RoleId"); });

            
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Items)
                .WithOne(i => i.Reservation)
                .HasForeignKey(i => i.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}

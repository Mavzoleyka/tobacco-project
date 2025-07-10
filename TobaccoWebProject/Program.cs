using AuthDomain;
using AuthDomain.Querys;
using AuthDomain.Querys.Object;
using Data;
using Data.CigaretteRepositories;
using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigaretteManufacturer.Commands;
using Domain.CigaretteDomain.CigaretteManufacturer.Querys;
using Domain.CigaretteDomain.CigarettePhoto;
using Domain.CigaretteDomain.CigarettePhoto.Commands;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Querys;
using Domain.CigaretteDomain.CigaretteProduct;
using Domain.CigaretteDomain.CigaretteProduct.Commands;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Microsoft.EntityFrameworkCore;
using Service;
using System.Reflection;

using System.Security.Claims;

namespace TobaccoWebProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "AdminPolicy");
            });

            string? path = builder.Configuration.GetConnectionString("sql");
            if(path == null) throw new ArgumentNullException(nameof(path));
            builder.Services.AddDbContext<Connection>(row=>row.UseSqlServer(path));

            var domainAssembliesService = Directory
            .GetFiles(AppContext.BaseDirectory, "*Domain.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();

            var domainAssembliesRepository = Directory
            .GetFiles(AppContext.BaseDirectory, "Data.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();

            builder.Services.Scan(scan => scan.FromAssemblies(domainAssembliesService)
                                               .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
                                                   .AsImplementedInterfaces()
                                                   .WithScopedLifetime()
                                                    );

            builder.Services.Scan(scan => scan.FromAssemblies(domainAssembliesRepository)
                                               .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
                                                   .AsImplementedInterfaces()
                                                   .WithScopedLifetime()
                                                    );


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication("Cookies")
                            .AddCookie("Cookies", option =>
                            {
                                option.LoginPath = "/Account/Login";
                                option.AccessDeniedPath = "/Account/AccessDenied";
                            });

            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("AutorizationPolicy", policy =>
                {
                    policy.RequireClaim("role", "User", "Admin");
                });

                option.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireClaim("role", "Admin");
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Connection>();
                db.Database.Migrate();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();
            app.MapRazorPages();
            
            app.Run();
        }
    }
}

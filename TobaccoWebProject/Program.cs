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
<<<<<<< HEAD
using System.Reflection;
=======
>>>>>>> 4e86bfd6c1622bbd7a295b3f4737f3422283a18f
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

<<<<<<< HEAD
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

=======
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IQueryService<EntryDTO, User?>, AuntificationQueryService>();
            builder.Services.AddScoped<IQueryService<User, ClaimsPrincipal>, CreatePrincipalQueryService>();
            builder.Services.AddScoped<IQueryService<RegistrationDTO, User?>, RegistrationUserQueryService>();


            builder.Services.AddScoped<ICigaretteProductRepository, CigaretteProductRepository>();
            builder.Services.AddScoped<ICommandService<AddCigaretteProductDTO>, AddProductCommandService>();
            builder.Services.AddScoped<ICommandService<DeleteCigaretteProductDTO>, DeleteProductCommandService>();
            builder.Services.AddScoped<ICommandService<UpdateCigaretteProductDTO>,  UpdateProductCommandService>();
            builder.Services.AddScoped<IQueryService<GetByIdDTO, Task<CigaretteProductDTO>>, GetProductByIdQueryService>();
            builder.Services.AddScoped<IQueryService<All, Task<List<CigaretteProductDTO>>>, GetAllProductsQueryService>();
            builder.Services.AddScoped<IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>>, GetProductsByCategoryQueryService>();
            builder.Services.AddScoped<IQueryService<GetByIdDTOforQuery, Task<NameOfProductManufacturerDTO>>, GetNameOfProductManufacturerQueryService>();

            builder.Services.AddScoped<ICigaretteCategoryRepository, CigaretteCategoryRepository>();
            builder.Services.AddScoped<ICommandService<AddCigaretteCategoryDTO>, AddCategoryCommandService>();
            builder.Services.AddScoped<ICommandService<DeleteCategoryDTO>, DeleteCategoryCommandService>();
            builder.Services.AddScoped<ICommandService<UpdateCategoryDTO>, UpdateCategoryCommandService>();
            builder.Services.AddScoped<IQueryService<All, Task<List<CigaretteCategoryDTO>>>, GetAllCategoryQueryService>();
            builder.Services.AddScoped <IQueryService<GetByIdDTO, Task<CigaretteCategoryDTO>>, GetCategoryByIdQueryService>();

            builder.Services.AddScoped<ICigaretteManufacturerRepository, CigaretteManufacturerRepository>();
            builder.Services.AddScoped<ICommandService<AddCigaratteManufacturerDTO>, AddManufacturerCommandService>();
            builder.Services.AddScoped<ICommandService<DeleteManufacturerDTO>, DeleteManufacturerCommandService>();
            builder.Services.AddScoped<ICommandService<UpdateManufacturerDTO>, UpdateManufacturerCommandService>();
            builder.Services.AddScoped<IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>>, GetManufacturerByIdQueryService>();
            builder.Services.AddScoped<IQueryService<All, Task<List<CigaretteManufacturerDTO>>>, GeAllManufacturersQueryService>();

            builder.Services.AddScoped<ICigarettePhotoRepository, CigarettePhotoRepository>();
            builder.Services.AddScoped<ICommandService<AddCigarettePhotoDTO>, AddPhotoCommandService>();
            builder.Services.AddScoped<ICommandService<UpdatePhotoDTO>, UpdatePhotoCommandService>();
            builder.Services.AddScoped<ICommandService<DeletePhotoDTO>, DeletePhotoCommandService>();
            builder.Services.AddScoped<IQueryService<GetByIdDTO, Task<CigarettePhotoDTO>>, GetPhotoByIdQueryService>();
            builder.Services.AddScoped<IQueryService<All, Task<List<CigarettePhotoDTO>>>, GetAllPhotosQueryService>();
            builder.Services.AddScoped<ICommandService<GetByIdPhotoDTO>, AddMainPhotoCommandService>();
>>>>>>> 4e86bfd6c1622bbd7a295b3f4737f3422283a18f

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

<<<<<<< HEAD
             var app = builder.Build();
=======
            var app = builder.Build();
>>>>>>> 4e86bfd6c1622bbd7a295b3f4737f3422283a18f
            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();
            app.MapRazorPages();
            
            app.Run();
        }
    }
}

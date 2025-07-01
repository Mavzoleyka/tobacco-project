using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using TobaccoWebProject.Pages.Admin.Products.Model;

namespace TobaccoWebProject.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly IQueryService<All, Task<List<CigaretteCategoryDTO>>> _getAllCategoriesQuery;
        private readonly IQueryService<All, Task<List<CigaretteManufacturerDTO>>> _getAllManufacturerQuery;
        private readonly IQueryService<All, Task<List<CigaretteProductDTO>>> _getAllProductQuery;
        private readonly ICommandService<DeleteCigaretteProductDTO> _deleteProductCommand;
        public IndexModel(IQueryService<All, Task<List<CigaretteCategoryDTO>>> getAllCategoriesQuery,
                          IQueryService<All, Task<List<CigaretteManufacturerDTO>>> getAllManufacturerQuery,
                          IQueryService<All, Task<List<CigaretteProductDTO>>> getAllProductQuery,
                          ICommandService<DeleteCigaretteProductDTO> deleteProductCommand)
        {
            ArgumentNullException.ThrowIfNull(getAllCategoriesQuery, nameof(getAllCategoriesQuery));
            ArgumentNullException.ThrowIfNull(getAllManufacturerQuery, nameof(getAllManufacturerQuery));
            ArgumentNullException.ThrowIfNull(getAllProductQuery, nameof(getAllProductQuery));
            ArgumentNullException.ThrowIfNull(deleteProductCommand, nameof(deleteProductCommand));
            _getAllCategoriesQuery = getAllCategoriesQuery;
            _getAllManufacturerQuery = getAllManufacturerQuery;
            _getAllProductQuery = getAllProductQuery;
            _deleteProductCommand = deleteProductCommand;
        }

        public List<ProductViewDTO> Products { get; set; } = new();
        public async Task OnGetAsync()
        {
            var products = await _getAllProductQuery.Execute(new All());
            var categories = await _getAllCategoriesQuery.Execute(new All());
            var manufacturers = await _getAllManufacturerQuery.Execute(new All());

            Products = products.Select(row=>new ProductViewDTO
            {
                Id = row.Id,
                Name = row.Name,
                Brand = row.Brand,
                CategoryName = categories.FirstOrDefault(c=>c.Id==row.CategoryId)?.Name ?? "-",
                ManufacturerName = manufacturers.FirstOrDefault(m=>m.Id ==  row.ManufacturerId)?.Name ?? "-"
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _deleteProductCommand.Execute(new DeleteCigaretteProductDTO() { Id = id });
            return RedirectToPage();
        }
    }
}

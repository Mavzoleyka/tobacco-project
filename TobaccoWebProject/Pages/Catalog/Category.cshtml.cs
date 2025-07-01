using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using TobaccoWebProject.Pages.Catalog.Model;

namespace TobaccoWebProject.Pages.Catalog
{
    public class CategoryModel : PageModel
    {
        private readonly IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>> _getProductsByCategoryQuery;
        public CategoryModel(IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>>
                             getProductsByCategoryQuery)
        {
            ArgumentNullException.ThrowIfNull(getProductsByCategoryQuery, nameof(getProductsByCategoryQuery));
            _getProductsByCategoryQuery = getProductsByCategoryQuery;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public string CategoryName { get; set; } = "";
        public List<ProductPreviewModel> Products { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            var products = await _getProductsByCategoryQuery.Execute(new GetByIdDTOforQuery { CategoryId = Id });
            if (products == null) return NotFound();

            Products = products.Select(p => new ProductPreviewModel
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand ?? "Без бренда",
                MainImage = p.CigarettesPhotos.FirstOrDefault(ph => ph.IsMain)?.ImageURL ?? "images/placeholder.png"
            }).ToList();

            CategoryName = products.FirstOrDefault()?.CategoryName ?? "Неизвестно";

            return Page();
        }
    }
}

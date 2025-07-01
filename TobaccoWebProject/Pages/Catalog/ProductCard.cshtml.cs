using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Catalog
{
    public class ProductCardModel : PageModel
    {
        private readonly IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> _getProductById;
        private readonly IQueryService<GetByIdDTOforQuery, Task<NameOfProductManufacturerDTO>> _getNameOfProductManufacturerQuery;
        public ProductCardModel(IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> getProductById,
            IQueryService<GetByIdDTOforQuery, Task<NameOfProductManufacturerDTO>> getNameOfProductManufacturerQuery)
        {
            ArgumentNullException.ThrowIfNull(getProductById, nameof(getProductById));
            ArgumentNullException.ThrowIfNull(getNameOfProductManufacturerQuery, nameof(getNameOfProductManufacturerQuery));
            _getProductById = getProductById;
            _getNameOfProductManufacturerQuery = getNameOfProductManufacturerQuery;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string MainImage { get; set; } = "images/placeholder.png";
        public List<string> OtherImages { get; set; } = new();
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var product = await _getProductById.Execute(new GetByIdDTO { Id = Id });
            if(product == null) return NotFound();

            var manufacturerName = await _getNameOfProductManufacturerQuery.Execute(new GetByIdDTOforQuery() { CategoryId = Id });
            Name = product.Name;
            Description = product.Description ?? "";
            Brand = product.Brand ?? "Без бренда";
            Manufacturer = manufacturerName.ManufacturerName ?? "Производитель не указан";
            Stock = product.Stock ?? 0;
            CategoryId = product.CategoryId;

            MainImage = product.CigarettesPhotos.FirstOrDefault(row => row.IsMain)?.ImageURL
                        ?? product.CigarettesPhotos.FirstOrDefault()?.ImageURL
                        ?? "images/placeholder.png";

            OtherImages = product.CigarettesPhotos
                          .Where(row => !row.IsMain)
                          .Select(row => row.ImageURL)
                          .ToList();
            return Page();
        }
    }
}

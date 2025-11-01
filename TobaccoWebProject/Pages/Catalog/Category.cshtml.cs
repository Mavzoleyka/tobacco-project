using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Domain.UserDomain;
using Domain.UserDomain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using TobaccoWebProject.Pages.Catalog.Model;

namespace TobaccoWebProject.Pages.Catalog
{
    public class CategoryModel : PageModel
    {
        private readonly IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>> _getProductsByCategoryQuery;
        private readonly CartService _cartService;

        public CategoryModel(
            IQueryService<GetByIdDTOforQuery, Task<List<CigaretteProductDTO>>> getProductsByCategoryQuery,
            CartService cartService)
        {
            ArgumentNullException.ThrowIfNull(getProductsByCategoryQuery, nameof(getProductsByCategoryQuery));
            ArgumentNullException.ThrowIfNull(cartService, nameof(cartService));

            _getProductsByCategoryQuery = getProductsByCategoryQuery;
            _cartService = cartService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;
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
                MainImage = p.CigarettesPhotos.FirstOrDefault(ph => ph.IsMain)?.ImageURL ?? "images/placeholder.png",
                Price = p.Price ?? 0
            }).ToList();

            CategoryName = products.FirstOrDefault()?.CategoryName ?? "Неизвестно";

            return Page();
        }

        //  Добавляем метод для кнопки “В корзину”
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            // Получаем все товары категории
            var products = await _getProductsByCategoryQuery.Execute(new GetByIdDTOforQuery { CategoryId = Id });
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return NotFound();

            _cartService.AddToCart(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price ?? 0,
                Quantity = 1
            });

            TempData["Message"] = $"{product.Name} добавлен в корзину!";
            return RedirectToPage(new { id = Id });
        }
    }
}
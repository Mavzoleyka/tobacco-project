using Domain.UserDomain;
using Domain.UserDomain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;

        public List<CartItem> Cart { get; set; } = new();

        public IndexModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public void OnGet()
        {
            Cart = _cartService.GetCart();
        }

        public IActionResult OnPostRemove(int productId)
        {
            _cartService.Remove(productId);
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            _cartService.Clear();
            return RedirectToPage();
        }

        
    }
}

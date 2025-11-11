using Domain.UserDomain.Services;
using Microsoft.AspNetCore.Mvc;

namespace TobaccoWebProject.ViewComponents
{
    public class CartWidgetViewComponent : ViewComponent
    {
        private readonly CartService _cartService;
        public CartWidgetViewComponent(CartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var cart = _cartService.GetCart();
            var count = cart.Sum(x => x.Quantity);
            return View(count);
        }
    }
}

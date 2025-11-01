using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;



namespace Domain.UserDomain.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public List<CartItem> GetCart()
        {
            var data = Session.GetString(SessionKey);
            return data == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(data)!;
        }

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                cart.Add(item);

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.ProductId == productId);
            SaveCart(cart);
        }

        public void Clear() => Session.Remove(SessionKey);

        private void SaveCart(List<CartItem> cart)
        {
            Session.SetString(SessionKey, JsonSerializer.Serialize(cart));
        }

        public void Remove(int productId)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
                cart.Remove(existing);
            SaveCart(cart);
        }

        public decimal GetTotal() => GetCart().Sum(x => x.Total);
    }
}

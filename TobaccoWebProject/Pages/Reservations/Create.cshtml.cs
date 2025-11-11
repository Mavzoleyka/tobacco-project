using Data;
using Domain.UserDomain.Commands.Object;
using Domain.UserDomain.Commands.Reservations;
using Domain.UserDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TobaccoWebProject.Pages.Reservations
{
    public partial class CreateModel : PageModel
    {
        
            private readonly IMediator _mediator;
            private readonly CartService _cartService;

        public CreateModel(IMediator mediator, CartService cartService)
            {
                _mediator = mediator;
                _cartService = cartService;
        }

            [BindProperty]
            public ReservationInputModel Input { get; set; } = new();

            [TempData]
            public string? SuccessMessage { get; set; }

            [TempData]
            public string? ErrorMessage { get; set; }

            public void OnGet()
            {
                if (Input.PickupDateTime == default)
                {
                    Input.PickupDateTime = DateTime.Now.AddMinutes(30);
                }
            }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var clientIdClaim = User.FindFirst("ClientId");
            if (clientIdClaim == null)
            {
                TempData["ErrorMessage"] = "Не удалось определить пользователя. Пожалуйста, войдите снова.";
                return RedirectToPage("/Account/Login");
            }

            int clientId = int.Parse(clientIdClaim.Value);

           
            var cart = _cartService.GetCart();

            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Корзина пуста. Добавьте товары перед бронированием.";
                return RedirectToPage("/Catalog/Index");
            }

            
            var dto = new CreateReservationDTO
            {
                ClientId = clientId,
                PickupDateTime = Input.PickupDateTime,
                Comment = Input.Comment,
                Items = cart.Select(c => new CreateReservationItemDTO
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    PriceAtBooking = c.Price
                }).ToList()
            };

            try
            {
                int reservationId = await _mediator.Send(new CreateReservationCommand(dto));

                _cartService.Clear();

                TempData["SuccessMessage"] = $"Бронь №{reservationId} успешно создана!";
                return RedirectToPage("/Reservations/My");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"[DB ERROR] {dbEx.InnerException?.Message ?? dbEx.Message}");
                TempData["ErrorMessage"] = "Не удалось создать бронь. Проверьте данные и попробуйте снова.";
                return RedirectToPage("/Reservations/Create");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                TempData["ErrorMessage"] = "Произошла ошибка при создании брони.";
                return RedirectToPage("/Reservations/Create");
            }
        }
    
        
        
    }
}

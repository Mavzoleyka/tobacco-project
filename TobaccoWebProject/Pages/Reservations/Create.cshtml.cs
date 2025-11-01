using Domain.UserDomain.Commands.Object;
using Domain.UserDomain.Commands.Reservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TobaccoWebProject.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        
            private readonly IMediator _mediator;

            public CreateModel(IMediator mediator)
            {
                _mediator = mediator;
            }

            [BindProperty]
            public ReservationInputModel Input { get; set; } = new();

            [TempData]
            public string? SuccessMessage { get; set; }

            [TempData]
            public string? ErrorMessage { get; set; }

            public void OnGet()
            {
                
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                    return Page();

                try
                {
                    var dto = new CreateReservationDTO
                    {
                        ClientId = 1, 
                        PickupDateTime = Input.PickupDateTime,
                        Comment = Input.Comment,
                        Items = new List<CreateReservationItemDTO>()
                    };

                    var command = new CreateReservationCommand(dto);

                    int reservationId = await _mediator.Send(command);

                    SuccessMessage = $"Бронь №{reservationId} успешно создана!";
                    return RedirectToPage("/Reservations/Success");
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Ошибка при создании брони: " + ex.Message;
                    return Page();
                }
            }

            public class ReservationInputModel
            {
                [Required(ErrorMessage = "Укажите время самовывоза")]
                [Display(Name = "Время самовывоза")]
                public DateTime PickupDateTime { get; set; }

                [MaxLength(100)]
                [Display(Name = "Комментарий (необязательно)")]
                public string? Comment { get; set; }
            }
        
        
    }
}

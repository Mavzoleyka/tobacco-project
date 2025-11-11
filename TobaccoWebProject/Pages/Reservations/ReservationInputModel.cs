using System.ComponentModel.DataAnnotations;

namespace TobaccoWebProject.Pages.Reservations
{
    public partial class CreateModel
    {
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Reservations
{
    public class SuccessModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ReservationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime PickupDateTime { get; set; }
        public void OnGet()
        {
        }
    }
}

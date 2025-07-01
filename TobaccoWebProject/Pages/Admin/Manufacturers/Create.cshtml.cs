using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Admin.Manufacturers
{
    public class CreateModel : PageModel
    {
        private readonly ICommandService<AddCigaratteManufacturerDTO> _createManufacService;
        public CreateModel(ICommandService<AddCigaratteManufacturerDTO> createManufacService)
        {
            ArgumentNullException.ThrowIfNull(createManufacService, nameof(createManufacService));
            _createManufacService = createManufacService;
        }

        [BindProperty]
        public AddCigaratteManufacturerDTO Manufacturer { get; set; } = new();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await _createManufacService.Execute(Manufacturer);

            return RedirectToPage("./Index");
        }
    }
}

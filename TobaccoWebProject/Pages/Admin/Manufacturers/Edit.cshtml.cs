using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Admin.Manufacturers
{
    public class EditModel : PageModel
    {
        private readonly IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>> _getManufacByIdService;
        private readonly ICommandService<UpdateManufacturerDTO> _updateManufacService;
        public EditModel(ICommandService<UpdateManufacturerDTO> updateManufacService,
            IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>> getManufacByIdService)
        {
            ArgumentNullException.ThrowIfNull(updateManufacService, nameof(updateManufacService));
            ArgumentNullException.ThrowIfNull(getManufacByIdService, nameof(getManufacByIdService));
            _updateManufacService = updateManufacService;
            _getManufacByIdService = getManufacByIdService;
        }

        [BindProperty]
        public UpdateManufacturerDTO Manufacturer { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if(id<=0) return NotFound("Нет такого id");
            
            var manufacturer = await _getManufacByIdService.Execute(new GetByIdDTO { Id = id });
            if (manufacturer == null) return NotFound();

            Manufacturer = new UpdateManufacturerDTO
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _updateManufacService.Execute(Manufacturer);
            return RedirectToPage("./Index");
        }
    }
}

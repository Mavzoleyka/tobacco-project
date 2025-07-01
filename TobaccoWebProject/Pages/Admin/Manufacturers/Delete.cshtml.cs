using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Admin.Manufacturers
{
    public class DeleteModel : PageModel
    {
        private readonly IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>> _getManufacByIdQuery;
        private readonly ICommandService<DeleteManufacturerDTO> _deleteManufacCommand;
        public DeleteModel(IQueryService<GetByIdDTO, Task<CigaretteManufacturerDTO>> getManufacByIdQuery,
            ICommandService<DeleteManufacturerDTO> deleteManufacCommand)
        {
            ArgumentNullException.ThrowIfNull(getManufacByIdQuery, nameof(getManufacByIdQuery));
            ArgumentNullException.ThrowIfNull(deleteManufacCommand, nameof(deleteManufacCommand));
            _getManufacByIdQuery = getManufacByIdQuery;
            _deleteManufacCommand = deleteManufacCommand;
        }

         public CigaretteManufacturerDTO? Manufacturer { get; set; }    

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0) return NotFound("Нет такого id");
            Manufacturer = await _getManufacByIdQuery.Execute(new GetByIdDTO { Id = id });
            if (Manufacturer == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _deleteManufacCommand.Execute(new DeleteManufacturerDTO { Id = id });
            return RedirectToPage("./Index");
        }
    }
}

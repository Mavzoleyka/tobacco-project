using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IQueryService<GetByIdDTO, Task<CigaretteCategoryDTO>> _getByIdCategoryQuery;
        private readonly ICommandService<UpdateCategoryDTO> _updateCategoryQuery;
        public EditModel(IQueryService<GetByIdDTO, Task<CigaretteCategoryDTO>> getByIdCategoryQuery,
                        ICommandService<UpdateCategoryDTO> updateCategoryQuery)
        {
            ArgumentNullException.ThrowIfNull(getByIdCategoryQuery, nameof(getByIdCategoryQuery));
            ArgumentNullException.ThrowIfNull(updateCategoryQuery, nameof(updateCategoryQuery));
            _getByIdCategoryQuery = getByIdCategoryQuery;
            _updateCategoryQuery = updateCategoryQuery;
        }

        [BindProperty]
        public UpdateCategoryDTO Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0) return NotFound("Нет такого id");

            var category = await _getByIdCategoryQuery.Execute(new GetByIdDTO() { Id = id });
            if (category == null) return NotFound();

            Input = new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _updateCategoryQuery.Execute(Input);
            return RedirectToPage("./Index");
        }
    }
}

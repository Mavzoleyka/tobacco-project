using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICigaretteCategoryRepository _repository;
        public CreateModel(ICigaretteCategoryRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        [BindProperty]
        public AddCigaretteCategoryDTO Input { get; set; } = new();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var category = new AddCigaretteCategoryDTO
            {
                Name = Input.Name,
            };

            await _repository.AddAsync(category);
            return RedirectToPage("./Index");
        }
    }
}

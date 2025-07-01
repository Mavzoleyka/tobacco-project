using Domain.CigaretteDomain.CigaretteCategorie;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TobaccoWebProject.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICigaretteCategoryRepository _repository;
        public IndexModel(ICigaretteCategoryRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            _repository = repository;
        }

        public List<CigaretteCategoryDTO> Categories { get; set; } = new();
        public async Task OnGetAsync()
        {
            Categories = await _repository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _repository.DeleteAsync(new DeleteCategoryDTO() { Id = id});
            return RedirectToPage();
        }
    }
}

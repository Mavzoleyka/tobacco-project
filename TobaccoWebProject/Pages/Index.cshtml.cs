using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IQueryService<All, Task<List<CigaretteCategoryDTO>>> _getAllCategoriesQuery;
        public IndexModel(IQueryService<All, Task<List<CigaretteCategoryDTO>>> getAllCategoriesQuery)
        {
            ArgumentNullException.ThrowIfNull(getAllCategoriesQuery, nameof(getAllCategoriesQuery));
            _getAllCategoriesQuery = getAllCategoriesQuery;
        }

        public List<CigaretteCategoryDTO> Categories { get; set; } = new();
        public async Task OnGetAsync()
        {
            Categories = await _getAllCategoriesQuery.Execute(new All());
        }
    }
}

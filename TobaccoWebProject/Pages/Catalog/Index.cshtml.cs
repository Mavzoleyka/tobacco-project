using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Runtime.CompilerServices;

namespace TobaccoWebProject.Pages.Catalog
{
    public class IndexModel : PageModel
    {
        private readonly IQueryService<All, Task<List<CigaretteCategoryDTO>>> _getAllCategoryQuery;
        public IndexModel(IQueryService<All, Task<List<CigaretteCategoryDTO>>> getAllCategoryQuery)
        {
            ArgumentNullException.ThrowIfNull(getAllCategoryQuery, nameof(getAllCategoryQuery));
            _getAllCategoryQuery = getAllCategoryQuery;
        }
        public List<CigaretteCategoryDTO> Categories { get; set; } = new List<CigaretteCategoryDTO>();
        public async Task OnGet()
        {
            Categories = await _getAllCategoryQuery.Execute(new All());
        }
    }
}


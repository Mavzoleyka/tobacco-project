using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Admin.Manufacturers
{
    public class IndexModel : PageModel
    {
        private readonly IQueryService<All, Task<List<CigaretteManufacturerDTO>>> _getAllManufacQuery;
        public IndexModel(IQueryService<All, Task<List<CigaretteManufacturerDTO>>> getAllManufacQuery)
        {
            ArgumentNullException.ThrowIfNull(getAllManufacQuery, nameof(getAllManufacQuery));
            _getAllManufacQuery = getAllManufacQuery;
        }

        public List<CigaretteManufacturerDTO> Manufacturers { get; set; } = new();

        public async Task OnGetAsync()
        {
            Manufacturers = await _getAllManufacQuery.Execute(new All());
        }
    }
}

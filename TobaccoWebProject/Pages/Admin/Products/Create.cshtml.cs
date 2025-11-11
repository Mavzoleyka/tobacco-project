using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;

namespace TobaccoWebProject.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IQueryService<All, Task<List<CigaretteCategoryDTO>>> _getAllCategoriesQuery;
        private readonly IQueryService<All, Task<List<CigaretteManufacturerDTO>>> _getAllManufacturerQuery;
        private readonly ICommandService<AddCigaretteProductDTO> _addProductCommand;
        public CreateModel(IQueryService<All, Task<List<CigaretteCategoryDTO>>> getAllCategoriesQuery,
                           IQueryService<All, Task<List<CigaretteManufacturerDTO>>> getAllManufacturerQuery,
                           ICommandService<AddCigaretteProductDTO> addProductCommand)
        {
            ArgumentNullException.ThrowIfNull(getAllCategoriesQuery, nameof(getAllCategoriesQuery));
            ArgumentNullException.ThrowIfNull(getAllManufacturerQuery, nameof(getAllManufacturerQuery));
            ArgumentNullException.ThrowIfNull(addProductCommand, nameof(addProductCommand));
            _getAllCategoriesQuery = getAllCategoriesQuery;
            _getAllManufacturerQuery = getAllManufacturerQuery;
            _addProductCommand = addProductCommand;
        }

        [BindProperty]
        public CigaretteProductDTO Input { get; set; } = new();

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Manufacturers { get; set; } = new List<SelectListItem>();
        public async Task OnGetAsync()
        {
            Categories = (await _getAllCategoriesQuery.Execute(new All()))
                                                      .Select(c => new SelectListItem 
                                                      { Value = c.Id.ToString(), Text = c.Name });
            Manufacturers = (await _getAllManufacturerQuery.Execute(new All())) .Select(m => new SelectListItem 
                                                        { Value = m.Id.ToString(), Text= m.Name });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }
            var product = new AddCigaretteProductDTO
            {
                Name = Input.Name,
                Description = Input.Description,
                Stock = Input.Stock,
                Brand = Input.Brand,
                Price = Input.Price,
                CategoryId = Input.CategoryId,
                ManufacturerId = Input.ManufacturerId
            };
            await _addProductCommand.Execute(product);
            return RedirectToPage("./Index");
        }

    }
}

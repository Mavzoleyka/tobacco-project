using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigaretteCategorie.Querys.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using TobaccoWebProject.Pages.Admin.Products.Model;

namespace TobaccoWebProject.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> _getProductByIdQuery;
        private readonly IQueryService<All, Task<List<CigaretteCategoryDTO>>> _getAllCategoriesQuery;
        private readonly IQueryService<All, Task<List<CigaretteManufacturerDTO>>> _getAllManufacturersQuery;
        private readonly ICommandService<UpdateCigaretteProductDTO> _updateProductCommand;

        
        public EditModel(IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> getProductByIdQuery,
                          IQueryService<All, Task<List<CigaretteCategoryDTO>>> getAllCategoriesQuery,
                          IQueryService<All, Task<List<CigaretteManufacturerDTO>>> getAllManufacturersQuery,
                          ICommandService<UpdateCigaretteProductDTO> updateProductCommand)
        {
            ArgumentNullException.ThrowIfNull(getProductByIdQuery, nameof(getProductByIdQuery));
            ArgumentNullException.ThrowIfNull(getAllCategoriesQuery, nameof(getAllCategoriesQuery));
            ArgumentNullException.ThrowIfNull(getAllManufacturersQuery, nameof(getAllManufacturersQuery));
            ArgumentNullException.ThrowIfNull(updateProductCommand, nameof(updateProductCommand));
            _getProductByIdQuery = getProductByIdQuery;
            _getAllCategoriesQuery = getAllCategoriesQuery;
            _getAllManufacturersQuery = getAllManufacturersQuery;
            _updateProductCommand = updateProductCommand;
        }
        [BindProperty]
        public UpdateCigaretteProductDTO Input { get; set; } = new();

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Manufacturers { get; set; } = new List<SelectListItem>();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0) return NotFound("Нет такого id");

            var product = await _getProductByIdQuery.Execute(new GetByIdDTO() { Id = id });

            Input = new UpdateCigaretteProductDTO
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Brand = product.Brand,
                CategoryId = product.CategoryId,
                ManufacturerId = product.ManufacturerId,
                Stock = product.Stock,
            };

            await LoadSelectLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _updateProductCommand.Execute(Input);
            return RedirectToPage("./Index");


        }

        private async Task LoadSelectLists()
        {
            Categories = (await _getAllCategoriesQuery.Execute(new All()))
                                                      .Select(c => new SelectListItem
                                                      { Value = c.Id.ToString(), Text = c.Name });
            Manufacturers = (await _getAllManufacturersQuery.Execute(new All())).Select(m => new SelectListItem
            { Value = m.Id.ToString(), Text = m.Name });
        }
    }
}

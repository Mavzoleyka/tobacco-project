using Data.CigaretteTables;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace TobaccoWebProject.Pages.Photos
{
    public class UploadModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> _getProductByIdQuery;
        private readonly ICommandService<AddCigarettePhotoDTO> _addPhotoCommand;
        private readonly ICommandService<GetByIdPhotoDTO> _addMainPhotoCommand;
        private readonly ICommandService<DeletePhotoDTO> _deletePhotoCommand;
        public UploadModel(IWebHostEnvironment environment,
                           IQueryService<GetByIdDTO, Task<CigaretteProductDTO>> getProductByIdQuery,
                           ICommandService<AddCigarettePhotoDTO> addPhotoCommand,
                           ICommandService<GetByIdPhotoDTO> addMainPhotoCommand,
                           ICommandService<DeletePhotoDTO> deletePhotoCommand)
        {
            ArgumentNullException.ThrowIfNull(environment, nameof(environment));
            ArgumentNullException.ThrowIfNull(getProductByIdQuery, nameof(getProductByIdQuery));
            ArgumentNullException.ThrowIfNull(addPhotoCommand, nameof(addPhotoCommand));
            ArgumentNullException.ThrowIfNull(addMainPhotoCommand, nameof(addMainPhotoCommand));
            ArgumentNullException.ThrowIfNull(deletePhotoCommand, nameof(deletePhotoCommand));
            _environment = environment;
            _getProductByIdQuery = getProductByIdQuery;
            _addPhotoCommand = addPhotoCommand;
            _addMainPhotoCommand = addMainPhotoCommand;
            _deletePhotoCommand = deletePhotoCommand;
        }


        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }

        [BindProperty]
        public IFormFileCollection Photos { get; set; } = default!;

        public string ProductName { get; set; } = "";
        public List<CigarettePhotoDTO> ExistingPhotos { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int ProductId)
        {
            var product = await _getProductByIdQuery.Execute(new GetByIdDTO { Id = ProductId });
            if(product == null) return NotFound();

            
            ProductName = product.Name;
            ExistingPhotos = product.CigarettesPhotos.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(Photos ==null||Photos.Count==0)
            {
                ModelState.AddModelError("", "Выберите хотя бы одно фото");
                return Page();
            }

            //var product = await _getProductByIdQuery.Execute(new GetByIdDTO() { Id = ProductId });
            //if(product == null) return NotFound();

            var uploadDir = Path.Combine(_environment.WebRootPath, "images", "products");
            Directory.CreateDirectory(uploadDir);

            foreach (var photo in Photos)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var newPhoto = new AddCigarettePhotoDTO
                {
                    ImageURL = Path.Combine("images", "products", fileName).Replace("\\", "/"),
                    Caption = null,
                    IsMain = false,
                    ProductId = ProductId,
                };
                await _addPhotoCommand.Execute(newPhoto);
                
            }
            return RedirectToPage(new { ProductId = ProductId });
        }

        public async Task<IActionResult> OnPostMakeMainPhotoAsync(int id)
        {
            
            await _addMainPhotoCommand.Execute(new GetByIdPhotoDTO() { Id = id });
            return RedirectToPage(new { ProductId = ProductId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _deletePhotoCommand.Execute(new DeletePhotoDTO() { Id = id });
            return RedirectToPage(new { ProductId = ProductId });
        }
    }
}

namespace TobaccoWebProject.Pages.Admin.Products.Model
{
    public class ProductViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Brand { get; set; }
        public string CategoryName { get; set; } = "";
        public string ManufacturerName { get; set; } = "";
    }
}

namespace TobaccoWebProject.Pages.Catalog.Model
{
    public class ProductPreviewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string MainImage { get; set; } = "";
        public decimal? Price { get; set; }
    }
}

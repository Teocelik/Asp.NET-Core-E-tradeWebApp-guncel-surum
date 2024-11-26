namespace KendinInşaEtSonSurumWebApp.ViewModels
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public decimal RatingRate { get; set; }

        public int RatingCount { get; set; }
    }
}

namespace KendinInşaEtSonSurumWebApp.Models
{
    public class CardItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}

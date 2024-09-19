namespace KendinInşaEtSonSurumWebApp.Models
{
    public class Card
    {
        //ilk olarak, sepetteki ürünleri tutacağımız bir liste oluşturalım(otomatik özellikli)
        public List<CardItem> Items { get; set; } = new List<CardItem>();

        //sepete ürün ekleyecek method
        public void AddItem(Product product, int quantity)
        {
            //listede suan ekleyeceğimiz ürün var mı? kontrol edelim
            var existingItem = Items.FirstOrDefault(p => p.ProductId == product.Id);

            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CardItem
                {
                    Product = product,
                    ProductId = product.Id,
                    Quantity = quantity
                });
            }
        }

        //Sepetteki toplam tutarını hesaplayan property
        public decimal TotalPrice => Items.Sum(p => p.Product.Price * p.Quantity);

        //Sepetteki ürün sayısını hesaplayan property
        public int TotalItemCount => Items.Sum(p => p.Quantity);
    }
}

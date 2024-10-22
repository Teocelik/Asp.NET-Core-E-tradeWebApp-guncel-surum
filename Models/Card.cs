namespace KendinInşaEtSonSurumWebApp.Models
{
    //Sepet ve sepetin işleyişi:
    public class Card
    {
        //İlk olarak otomatik özellikli bir liste oluşturalım.(Bu liste sepetteki ürünleri listeleyecek.
        //Bir nevi sepetin kendisidir diyebiliriz)
        public List<CardItem> Items { get; set; } = new List<CardItem>();

        //Items listesine ürün ekleyecek bir method tasarlayalım(kodlayalım)
        //Ürünü miktarına göre listeye ekler.
        public void AddItem(Product product, int quantity)
        {
            //Ürünü sepete eklemeden önce, sepete ekleyeceğimiz üründen var mı yok mu kontrol etmemiz lazım.
            var existingItem = Items.FirstOrDefault(p => p.ProductId == product.Id);

            //Eğer sepete ekleyeceğimiz ürün sepette zaten varsa, o ürünün sadece miktarını arttıracağız.
            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            //Sepete ekleyeceğimiz ürün sepette yoksa, yeni bir ürün nesnesi oluşturup sepete ekleyeceğiz.
            else
            {
                Items.Add(new CardItem
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    Product = product
                });
            }
        }
    }
}

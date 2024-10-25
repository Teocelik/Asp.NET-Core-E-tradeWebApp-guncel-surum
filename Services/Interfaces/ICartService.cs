using KendinInşaEtSonSurumWebApp.Models;

namespace KendinInşaEtSonSurumWebApp.Services.Interfaces
{
    public interface ICardService
    {
        //Sepetteki ürünleri listeleme methodu
        Card GetCard();
        //sepete ürün ekleme methodu
        void AddItem(Product product, int quantity);
        //sepetten ürün silme methodu
        void RemoveItem(int id);
        //Tüm ürünleri sepetten temizleme methodu
        void ClearCard();
    }
}

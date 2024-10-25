using KendinInşaEtSonSurumWebApp.Models;

namespace KendinInşaEtSonSurumWebApp.Services.Interfaces
{
    public interface ICardService
    {
        //Sepetteki ürünleri listeleme methodu
        Card Get();
        //sepete ürün ekleme methodu
        void Add(Product product, int quantity);
        //sepetten ürün silme methodu
        void Remove(int id);
        //Tüm ürünleri sepetten temizleme methodu
        void Clear();
    }
}

using KendinInşaEtSonSurumWebApp.Models;

namespace KendinInşaEtSonSurumWebApp.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product entity);
        void DeleteProduct(int id);
        void UpdateProduct(Product entity);  
    }
}

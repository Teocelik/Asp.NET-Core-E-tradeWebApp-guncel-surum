using KendinInşaEtSonSurumWebApp.DataAccess.Repositories;
using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.Services.Interfaces;

namespace KendinInşaEtSonSurumWebApp.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product entity)
        {

            _productRepository.Add(entity);
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void UpdateProduct(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}

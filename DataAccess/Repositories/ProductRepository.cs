using KendinInşaEtSonSurumWebApp.Models;

namespace KendinInşaEtSonSurumWebApp.DataAccess.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Product entity)
        {
            _appDbContext.Products.Add(entity);
        }

        public void Delete(int id)
        {
            _appDbContext.Remove(id);
        }

        public List<Product> GetAll()
        {
            return _appDbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product entity)
        {
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}

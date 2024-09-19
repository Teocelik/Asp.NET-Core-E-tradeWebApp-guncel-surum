namespace KendinInşaEtSonSurumWebApp.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
    }
}

using KendinInşaEtSonSurumWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KendinInşaEtSonSurumWebApp.DataAccess
{
    public class AppDbContext: IdentityDbContext<User>
    {
        //veritabanı tablosuna karşılık gelen bir DbSet oluştur!
        public DbSet<Product> Products { get; set; }

        //Bu constructor, DbContext sınıfına ayarları geçirir.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}

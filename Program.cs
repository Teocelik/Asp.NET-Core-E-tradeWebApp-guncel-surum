using KendinInþaEtSonSurumWebApp.DataAccess;
using KendinInþaEtSonSurumWebApp.DataAccess.Repositories;
using KendinInþaEtSonSurumWebApp.Models;
using KendinInþaEtSonSurumWebApp.Services.Concrete;
using KendinInþaEtSonSurumWebApp.Services.Concretes;
using KendinInþaEtSonSurumWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//veritabaný baðlantýsýný servis olarak container'a ekleyelim.(DI)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnect")));

//Identity kütüphanesi ekleme sonrasý container'a eklenen servis
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>() //AppDbContext sýnýfýný belirler.
                .AddDefaultTokenProviders(); // Parola sýfýrlama, e-posta doðrulama gibi iþlemler için gerekli olan varsayýlan token saðlayýcýlarýný ekler.



//IProductServisini container'a ekleyelim
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CardService>();
//

//HttpContextAccessor ile Session(oturum)'dan sepet bilgisini almak için bu servisleri container'a ekledim.
#region Nasýl çalýþýr ve birbirleriyle olan baðlarý nedir?
/* 
   HTTP : Web üzerinden veri iletmek için kullanýlan protokoldür. Ýstemci (client) ile sunucu (server) arasýndaki iletiþimi saðlar.
   HttpContext : bu nesne, bir HTTP isteði sýrasýnda kullanýlan kullanýcý oturum verilerini, istek/yanýt bilgilerini ve diðer isteðe özel bilgileri içerir.

   IHttpContextAccessor ile HttpContext nesnesine eriþim saðlýyoruz. (Yani kullanýcýnýn istek/yanýt bilgilerine eriþim saðlayabiliyoruz)
   
   Problemimiz neydi? : Kullanýcý bilgilerine oturum boyunca eriþmek ve yönetmek.
   
   - her istek sýrasýnda veya sayfa yenilemelerde vs. gibi durumlarda kullanýcý verilerini kaybolur.
   - bu verileri oturumda tutmak ve oturum boyunca eriþmek için ise ISession arayüzünü kullanýyoruz.(Yani, verilere eriþmek için oturumda saklýyoruz)
     
 */
#endregion
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//oturumlarýn çalýþabilmesi için
app.UseSession();

//gelen isteklerin nasýl karþýlanacaðý yer.!
app.MapDefaultControllerRoute();

app.Run();

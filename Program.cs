using KendinIn�aEtSonSurumWebApp.DataAccess;
using KendinIn�aEtSonSurumWebApp.DataAccess.Repositories;
using KendinIn�aEtSonSurumWebApp.Models;
using KendinIn�aEtSonSurumWebApp.Services.Concrete;
using KendinIn�aEtSonSurumWebApp.Services.Concretes;
using KendinIn�aEtSonSurumWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//veritaban� ba�lant�s�n� servis olarak container'a ekleyelim.(DI)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnect")));

//Identity k�t�phanesi ekleme sonras� container'a eklenen servis
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>() //AppDbContext s�n�f�n� belirler.
                .AddDefaultTokenProviders(); // Parola s�f�rlama, e-posta do�rulama gibi i�lemler i�in gerekli olan varsay�lan token sa�lay�c�lar�n� ekler.



//IProductServisini container'a ekleyelim
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CardService>();
//

//HttpContextAccessor ile Session(oturum)'dan sepet bilgisini almak i�in bu servisleri container'a ekledim.
#region Nas�l �al���r ve birbirleriyle olan ba�lar� nedir?
/* 
   HTTP : Web �zerinden veri iletmek i�in kullan�lan protokold�r. �stemci (client) ile sunucu (server) aras�ndaki ileti�imi sa�lar.
   HttpContext : bu nesne, bir HTTP iste�i s�ras�nda kullan�lan kullan�c� oturum verilerini, istek/yan�t bilgilerini ve di�er iste�e �zel bilgileri i�erir.

   IHttpContextAccessor ile HttpContext nesnesine eri�im sa�l�yoruz. (Yani kullan�c�n�n istek/yan�t bilgilerine eri�im sa�layabiliyoruz)
   
   Problemimiz neydi? : Kullan�c� bilgilerine oturum boyunca eri�mek ve y�netmek.
   
   - her istek s�ras�nda veya sayfa yenilemelerde vs. gibi durumlarda kullan�c� verilerini kaybolur.
   - bu verileri oturumda tutmak ve oturum boyunca eri�mek i�in ise ISession aray�z�n� kullan�yoruz.(Yani, verilere eri�mek i�in oturumda sakl�yoruz)
     
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

//oturumlar�n �al��abilmesi i�in
app.UseSession();

//gelen isteklerin nas�l kar��lanaca�� yer.!
app.MapDefaultControllerRoute();

app.Run();

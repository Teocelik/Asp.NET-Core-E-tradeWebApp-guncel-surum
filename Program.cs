using KendinInþaEtSonSurumWebApp.DataAccess;
using KendinInþaEtSonSurumWebApp.DataAccess.Repositories;
using KendinInþaEtSonSurumWebApp.Models;
using KendinInþaEtSonSurumWebApp.Services.Concrete;
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

//gelen isteklerin nasýl karþýlanacaðý yer.!
app.MapDefaultControllerRoute();

app.Run();

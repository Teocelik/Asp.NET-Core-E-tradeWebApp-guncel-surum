using KendinIn�aEtSonSurumWebApp.DataAccess;
using KendinIn�aEtSonSurumWebApp.DataAccess.Repositories;
using KendinIn�aEtSonSurumWebApp.Models;
using KendinIn�aEtSonSurumWebApp.Services.Concrete;
using KendinIn�aEtSonSurumWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//veritaban� ba�lant�s�n� servis olarak container'a ekleyelim.(DI)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnect")));

//
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();



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

//gelen isteklerin nas�l kar��lanaca�� yer.!
app.MapDefaultControllerRoute();

app.Run();

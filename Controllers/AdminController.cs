using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.Services.Interfaces;
using KendinInşaEtSonSurumWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        //Admin panelini açar
        public IActionResult Index()
        {
            return View();
        }

        //CreateViewModel'ın açılmasını sağlar
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //CreateViewModel'dan verileri alır ve yeni bir ürün oluşturur.
        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            //ilk olarak CreateViewModel'dan gelen verinin formatını doğrulayalım
            if(ModelState.IsValid)
            {
                try
                {
                    var newProduct = new Product
                    {
                        Title = model.Title,
                        Price = model.Price,
                        Description = model.Description,
                        Category = model.Category,
                        ImageUrl = model.ImageUrl,
                        RatingRate = model.RatingRate,
                        RatingCount = model.RatingCount
                    };
                    //Admin'e, Ürün eklendi mesajı göster!
                    TempData["SuccessMessage"] = "Ürün başarıyla eklendi!";
                    //ürünü ekle
                    _productService.AddProduct(newProduct);
                    //ürün sayfasına yönlendirelim
                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    //genel hata!
                    ModelState.AddModelError("", "Ürün eklenirken bir hata oluştu!" + ex.Message);
                }
            }
            return View(model);
        }

    }
}

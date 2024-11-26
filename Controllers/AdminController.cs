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

        //Create view'ini açar.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //CreateViewModel nesnesi sayesinde ürün verilerini alır ve yeni bir ürün oluşturur.
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

        //*******Update yapılanması(bir ürünün güncellenmesi)*********************

        //Ürünü id'ye göre alır ve view'de(form) görüntülenir.
        [HttpGet]
        public IActionResult Update(int id)
        {
            //ürünü id'ye göre aldım.
            var product = _productService.GetProductById(1);

            //Product nesnesini "UpdateProductViewModel" nesnesine dönüştürelim
            var model = new UpdateProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                RatingRate = product.RatingRate,
                RatingCount = product.RatingCount
            };
            return View(model);
        }

        //Değişiklikleri veritabanına kaydeder
        [HttpPost]
        public IActionResult Update(UpdateProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var updatedProduct = new Product
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Price = model.Price,
                        Description = model.Description,
                        Category = model.Category,
                        ImageUrl = model.ImageUrl,
                        RatingRate = model.RatingRate,
                        RatingCount = model.RatingCount
                    };
                    // ürün güncellendi bilgisi verelim
                    TempData["SuccessMessage"] = "Ürün başarıyla güncellendi!";
                    //ürünü güncelledim
                    _productService.UpdateProduct(updatedProduct);
                    

                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Ürün güncellenirken bir hata oluştu" + ex.Message);
                }
            }
            //formu dolu olarak tekrar geri gönderdim!
            return View(model);
        }

    }
}

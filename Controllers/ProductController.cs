using KendinInşaEtSonSurumWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            var p = product;
            return View(product);
        }
    }
}

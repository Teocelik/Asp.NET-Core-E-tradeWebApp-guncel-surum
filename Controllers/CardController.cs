using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class CardController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICardService _cardService;

        public CardController(IProductService productService, ICardService cardService)
        {
            _productService = productService;
            _cardService = cardService;
        }

        //sepetteki ürünleri liste olarak kullanıcıya return eder.
        public IActionResult Index()
        {
            var items = _cardService.Get();
            return View(items);
        }

        public IActionResult Add(int productId, int quantity)
        {
            //ilk olarak ekleyeceğimiz ürünü getirelim
            var product = _productService.GetProductById(productId);
            if(product != null)
            {
                _cardService.Add(product, quantity);
            }
            return RedirectToAction("Details", "Product", new {id = productId});
        }

        public IActionResult CheckOut()
        {
            return RedirectToAction("CheckOut", "CheckOut");
        }
    }
}

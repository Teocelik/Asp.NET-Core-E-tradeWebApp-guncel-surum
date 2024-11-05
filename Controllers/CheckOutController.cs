using KendinInşaEtSonSurumWebApp.Extensions;
using KendinInşaEtSonSurumWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;


namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly StripeSettings _options;
        
        public CheckOutController(IOptions<StripeSettings> options)
        {
            _options = options.Value;
            
        }



        public IActionResult Index()
        {
            //Stripe API anahtarını yapılandıralım(doğrulama için, odeme formuna yönlendirmesi için) => SecretKey ile bu doğrulama yapılır
            StripeConfiguration.ApiKey = _options.SecretKey;

            //Sepete ulaşalım.
            var card = HttpContext.Session.GetObjectFromJson<Models.Card>("Card");

            //Sepetin boş olup olmadığını kontrol edelim
            if(card == null || !card.Items.Any())
            {
                ViewBag.ErrorMessage = "Sepetiniz şuan boş!";
                return View("EmptyCart");
            }
            return View();
        }
    }
}

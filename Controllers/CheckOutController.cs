using KendinInşaEtSonSurumWebApp.Extensions;
using KendinInşaEtSonSurumWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using static System.Net.WebRequestMethods;


namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class CheckOutController : Controller
    {
        //field
        private readonly StripeSettings _stripeSettings;
        
        //Constructor
        public CheckOutController(IOptions<StripeSettings> options)//IOptions arabiribi ile stripeSettings ayarlarına erişim sağladım.
        {
            _stripeSettings = options.Value;
            
        }



        public IActionResult CheckOut()
        {
            //ilk olarak, secretKey anahtarımızı alalım.
            //SecretKey: ödeme formuna erişim ve doğrulama için gereklidir.
            //ödeme işlemleri yapabilmek için SecretKey'i yapılandıralım.
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            //Oturumda sakladığımız Sepet bilgilerine erişelim.
            var card = HttpContext.Session.GetObjectFromJson<Models.Card>("Card");

            //Sepetimizde ürün var mı kontrol edelim.
            if(card != null || !card.Items.Any())
            {
                ViewBag.ErrorMessage = "Sepetiniz şuan boş!";
                return View("EmptyCard");
            }

            //Projemizin üzerinde çalıştığı domain(alan)'i alalım.
            //Bu, kullanıcı başarılı veya başarısız bir ödeme gerçekleştirdiğinde
            //geri yönlendirileceği URL'leri oluşturmak için kullanacağız.

            var domain = "https://localhost:44324/";

            //ödeme oturumunu ayarlarını oluşturalım(SessionCreateOptions):
            //SessionCreateOptions: tüm oturum ayarlarını topluca yapılandırmamızı sağlar.
            var options = new SessionCreateOptions
            {
                //Ödeme yöntemi seçelim(burada sadece kredi kartı ile ödeme ekledim)
                PaymentMethodTypes = new List<string> { "card" },

                //SessionLineItemOptions nesnesi, Stripe Checkout oturumundaki bir satır öğesini temsil eder.
                //Yani SessionLineItemOptions nesnesi, Stripe CheckOut oturumunda her bir ürünü ve onun fiyatını içerir.
                LineItems = new List<SessionLineItemOptions>(),

                //ödeme işleminin nasıl ve hangi ortamda yapılacağını belirtelim.
                //Tek seferlik ödeme için Mode yapılanması..
                Mode = "payment",

                //Ödeme başarılı olduğunda yönlendirilecek url
                SuccessUrl = domain + "CheckOut/OrderConfirmation",

                //Ödeme başarısız olduğu durumda yönlendirileceğimiz url
                CancelUrl = domain + "CheckOut/Login"

            };
            return View();
        }
    }
}

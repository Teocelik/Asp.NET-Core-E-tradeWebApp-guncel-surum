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
            //bu metodun amacı: Stripe checkOut oturmunu başlatmak ve sepet bilgisini stripe ödeme formuna taşımak ve ödeme işlemlerini tamamlamak.

            //*yapılacaklar:
            //-------------

            //Stripe checkOut formuna erişmek ve ödeme doğrulamak için SecretKey anahtarını yapılandıralım.
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            //Oturumda sakladığımız sepet bilgisine erişelim
            var card = HttpContext.Session.GetObjectFromJson<Models.Card>("Card");

            //Sepette ürün var mı bilgisini kontrol edelim
            if(card == null || !card.Items.Any())
            {
                //mesaj
                ViewBag.ErrorMessage = "Sepetinizde şuan boş!";
                return View("EmptyCard");
            }

            //sepetin dolu olması halinde..

            //ilk olarak ödemenin başarılı veya başarısız durumunda kullanacağımız
            //ve bizi yönlendirecek alanı(domain) tamımlayalım
            var domain = "https://localhost:44324/";

            //Stripe CheckOut oturum ayarlarını yapılandıralım(SessionCreateOptions nesnesi)
            var options = new SessionCreateOptions
            {
                //Ödeme yöntemini ayarlayalım
                PaymentMethodTypes = new List<string> { "card" },

                //Ödeme oturumunda yer alacak ürün özelliklerinin yapılanması için
                //SessionLineItemOptions nesnesini kullandım.
                LineItems = new List<SessionLineItemOptions>(),

                //ödeme modunu yapılandıralım
                //Burada tek seferlik ödeme için payment modunu seçtim
                Mode = "payment",

                //Ödeme başarılı olduğunda yönlendirileceğimiz URL
                SuccessUrl = domain + "CheckOut/OrderConfirmation",

                //Ödeme başarısız olduğunda yönlendirileceğimiz URL
                CancelUrl = domain + "CheckOut/Login"
            };

            //Sepetimizdeki her bir ürünü LineItems listesine eklemek için döngü kullanalım.
            foreach(var item in card.Items)
            {
                //ürün özelliklerini yapılandıralım
                var sessionLineItem = new SessionLineItemOptions
                {
                    //ürün bilgilerini atayalım
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        //ürün fiyatı(kurış bazından belirledim)
                        UnitAmount = (long)(item.Product.Price * 100),
                        //para birimi
                        Currency = "usd",
                        //ürün ismi vs
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title,
                        },
                    },
                    //ürün miktarı
                    Quantity = item.Quantity,
                };
                //ürünü LineItems'e ekle
                options.LineItems.Add(sessionLineItem);
            }

            //Stripe oturumunu oluşturulalım
            var service = new SessionService();
            //yaptığımız ayarları oturuma verelim
            Session session = service.Create(options);

            //OrderConfirmation methodunda ödeme işleminin doğrulama sürecini kontrol etmek ve kullanıcıya doğru bilgi sunmak için oturum id'sini saklayalım
            HttpContext.Session.SetString("SessionId", session.Id);

            /*Ödeme sayfasına erişmek için Http yanıt başlığına 
            stripe url'sini ekleyelim(Bu bizi stripe ödeme sayfasına
            yönlendirecek url'yi http başlık kısmına ekler)*/
            Response.Headers.Append("Location", session.Url);

            //Bu bizi stripe ödeme sayfasına yönlendirecek
            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation()
        {
            //ilk olarak oturumumuzun id'sini alalım(ödeme işlem bilgilerine erişmek için)
            var sessionId = HttpContext.Session.GetString("SessionId");

            if (string.IsNullOrEmpty(sessionId))
            {
                return View("Login");
            }

            // yeni bir oturum servisi oluşturalım
            var service = new SessionService();

            //yeni oturum servisine oturumumuzun bilgilerini verelim.
            //Bu, Stripe üzerinden oturum bilgilerimize erişmemizi sağlar.
            var session = service.Get(sessionId);

            //ödeme başarılı ise..
            if (session.PaymentStatus == "paid")
            {
                return View("Success");
            }
            //değilse..
            else
            {
                return View("Login");
            }
        }
    }
}

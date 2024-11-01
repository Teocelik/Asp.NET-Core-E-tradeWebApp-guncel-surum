using KendinInşaEtSonSurumWebApp.Extensions;
using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.Services.Interfaces;

namespace KendinInşaEtSonSurumWebApp.Services.Concretes
{
    public class CardService : ICardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public CardService(IHttpContextAccessor httpContextAccessor)
        {
            //Burada, HttpContext'e erişim sağladım.
            //HttpContext, HTTP isteği ile ilgili bilgilere
            //(örneğin, istek başlıkları, kullanıcı, oturum vb.) erişim sağlar.
            _httpContextAccessor = httpContextAccessor;

            //Burada, kullanıcıya ait oturumda ve oturum boyunca saklanan verilere eişim sağladım.
            // httpContextAccessor ile HttpContext'e (yani kullanıcı bilgilerine) eriştim.
            //HttpContext ile session(oturum)'a erişim sağladım. Bu, oturumdaki verileri yönetmek
            //ve erişmek için kullanılır.
            //Yani, oturumda veri saklamak, okumak veya güncellemek için bu nesneyi kullanabiliriz.
            _session = httpContextAccessor.HttpContext.Session;

        }

        public void Add(Product product, int quantity)
        {
            //ilk olarak, sepeti listeleyen methodu çağıralım.
            var card = Get();

            //Card class'ının içinde yapılandırdığımız ürün ekleme methodunu(AddItem)
            //kullanarak sepete yeni ürünü ekleyelim.
            card.AddItem(product, quantity);

            //son olarak, yaptığımız değişiklikleri oturuma kaydedelim.(Json formatında saklıyoruz)
            _session.SetObjectFromJson("Card", card);// key: "Card", value : card
        }

        public void Clear()
        {
            //ilk olarak sepeti çağıralım.
            var card = Get();
            //Sepetin içindeki ürünleri silelim
            card.Items.Clear();
            //yaptığımız değişiklikleri oturuma kaydedelim
            _session.SetObjectFromJson("Card", card);
        }
        
        public Card Get()
        {
            //ISession arayüzünü, nesneleri oturumda Json formatında saklaması ve tekrar orjinal hale 
            //dönüştürmesi için genişletmiştik.(SessionExtensions)

            //Bu methodda, ilk olarak oturumda saklanılan sepet bilgisini alalım.
            //Nasıl yapacağız? : sepetteki ürünleri oturumda
            //Json formatında saklıyorduk(genişletme metodu ile(SetObjectAsJson)).
            //Şimdi bu ürünleri orjinal hallerine(nesneye) dönüştürerek çağıracağız.
            var card = _session.GetObjectFromJson<Card>("Card") ?? new Card();
            return card;
        }

        public void Remove(int id)
        {
            //belirli bir ürünü silmek için, ilk olarak sepeti çağıralım.
            var card = Get();
            //id üzerinden ürünü silelim.
            card.Remove(id);
            //Yaptığımız değişiklikleri oturuma kaydedelim.
            _session.SetObjectFromJson("Card", card);
        }
    }
}

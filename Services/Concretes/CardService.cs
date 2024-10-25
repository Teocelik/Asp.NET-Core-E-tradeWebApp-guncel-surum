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
            //HttpContext, HTTP isteği ile ilgili bilgilere (örneğin, istek başlıkları, kullanıcı, oturum vb.) erişim sağlar.
            _httpContextAccessor = httpContextAccessor;

            //Burada, kullanıcıya ait oturumda ve oturum boyunca saklanan verilere eişim sağladım.
            // httpContextAccessor ile HttpContext'e (yani kullanıcı bilgilerine) eriştim.
            //HttpContext ile session(oturum)'a erişim sağladım. Bu, oturumdaki verileri yönetmek ve erişmek için kullanılır.
            //Yani, oturumda veri saklamak, okumak veya güncellemek için bu nesneyi kullanabiliriz.
            _session = httpContextAccessor.HttpContext.Session;

        }

        public void Add(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Card Get()
        {
            //ISession arayüzünü, nesneleri oturumda Json formatında saklaması ve tekrar orjinal hale 
            //Dönüştürmesi için genişletmiştik.(SessionExtensions) (Yani ISession arayüzüne yeni methodlar eklemiştik)

            //Bu methodda, ilk olarak oturumda saklanılan sepet bilgisini alalım.
            //Nasıl yapacağız? : sepetteki ürünler oturumda Json formatında saklıyorduk(genişletme metodu ile(SetObjectAsJson)).
            //Şimdi bu ürünleri orjinal hallerine(nesneye) dönüştürerek sepette listeleyeceğiz.
            var card = _session.GetObjectFromJson<Card>("Card") ?? new Card();
            return card;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

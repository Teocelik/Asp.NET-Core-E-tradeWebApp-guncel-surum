# eCommerce Projesi

Bu proje, C# ve ASP.NET Core MVC kullanılarak geliştirilmiş basit bir e-ticaret uygulamasıdır. Proje, ürün listeleme, kullanıcı oturumu, sepet yönetimi ve ödeme işlemlerini içeren bir alışveriş deneyimi sunar.

## Proje Özellikleri

- **Ürün Listeleme**: Ürünler kategori bazlı veya tüm ürünler olarak listelenebilir.
- **Ürün Detayları**: Kullanıcılar seçilen ürünün detay sayfasına giderek, ürün bilgilerini inceleyebilir.
- **Kullanıcı Yönetimi**: ASP.NET Identity ile kullanıcı kayıt ve giriş işlemleri.
- **Sepet Yönetimi**: Kullanıcıların ürün ekleyip çıkarabileceği, ürün miktarını güncelleyebileceği bir alışveriş sepeti.
- **Ödeme İşlemleri**: Stripe API ile entegrasyon sağlanarak ödeme işlemi gerçekleştirilebilir.
- **Sipariş Takibi**: Kullanıcılar sipariş geçmişlerini görüntüleyebilir.

## Teknolojiler

Bu projede kullanılan ana teknolojiler ve araçlar:

- **C# & ASP.NET Core MVC** - Web uygulaması geliştirme
- **Entity Framework Core** - Veritabanı yönetimi ve ORM
- **ASP.NET Identity** - Kullanıcı kimlik doğrulama ve yönetimi
- **Stripe API** - Ödeme entegrasyonu


## Proje Yapısı

Proje, ASP.NET Core MVC yapısını temel alarak aşağıdaki gibi düzenlenmiştir:

- **Controllers**: Kullanıcı isteklerini karşılayıp yönlendirme yapan denetleyiciler. Örneğin:
  - `HomeController`: Ana sayfa ve genel sayfa yönlendirmelerini içerir.
  - `ProductController`: Ürün detayları ve listeleme işlevlerini yönetir.
  - `CartController`: Sepet işlemlerini yürütür.
  - `OrderController`: Sipariş yönetimi ve işlemlerini kontrol eder.

- **Models**: Veritabanında karşılığı olan ve uygulamanın veri modelini ve uygulamanın iş mantığını temsil eden sınıflar.
  - `Product`: Ürün bilgilerini tanımlar (ID, isim, fiyat vb.).
  - `CardItem`: Sepet öğesi özelliklerini(property) tanımlar (ürün, miktar, toplam fiyat).
  - `Card`: Sepet işleyişini içerir(eklenecek ürün ve miktarı, eklenen ürün sepette var mı?, toplam tutar hesaplama vb.).

- **Views**: Razor dosyalarını içerir; uygulamanın ön yüzünü ve kullanıcı arayüzünü tanımlar.
  - **Home**: Ana sayfa ve diğer genel sayfa şablonları.
  - **Products**: Ürünlerin listelendiği ve detay sayfalarının bulunduğu görünümler.
  - **Cart**: Sepet içeriğinin ve sepet işlemlerinin bulunduğu görünümler.
  - **Orders**: Sipariş geçmişi ve detay sayfaları.

- **Services**: İş mantığı sınıfları; veritabanı işlemlerini ve oturum yönetimini içerir.
  - `ProductService`: Ürünleri veritabanından alır ve işler.(CRUD işlemleri) 
  - `CartService`: Sepet yönetimi için işlevleri sağlar (ürün ekleme, çıkarma vb.).
  - `OrderService`: Sipariş oluşturma ve sipariş bilgilerini saklama işlevleri.

## Kullanım

Bu proje, kullanıcıların ürünleri kolayca listelemesini, sepete eklemesini ve sipariş işlemlerini yönetmesini sağlar:

1. **Kayıt Ol ve Giriş Yap**: 
   - Kullanıcılar sisteme kaydolabilir ve hesaplarıyla giriş yapabilirler.
   - ASP.NET Identity sistemi kullanıcı kimlik doğrulama işlemlerini yönetir.

2. **Ürünleri Listeleme ve Sepete Ekleme**:
   - Kullanıcılar listelenen ürünleri inceleyebilir ve beğendikleri ürünleri sepete ekleyebilir.
   - Ürün miktarları artırılabilir veya azaltılabilir.

3. **Sepet Yönetimi ve Ödeme**:
   - Sepet sayfasında, eklenen ürünlerin miktarları güncellenebilir veya ürünler sepetten kaldırılabilir.
   - Kullanıcılar sepeti onaylayarak ödeme işlemi başlatabilir (Stripe API ile ödeme işlemi).

4. **Sipariş Takibi**:
   - Kullanıcılar tamamladıkları siparişleri geçmiş siparişler olarak inceleyebilirler.
   - Sipariş detayları kullanıcıya ait bilgilerle birlikte saklanır.

## Lisans

Bu proje herhangi bir lisans'a sahip değildir.!

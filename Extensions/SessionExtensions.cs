using System.Text.Json;
namespace KendinInşaEtSonSurumWebApp.Extensions
{
    //Extension(Uzatma sınıflar) class'lar mevcut sınıfa dokunmadan yeni özellikler eklememizi sağlar.
    // this anahtar kelimesi ile belirttiğimiz türe özel genişletmeler yapabiliyoruz.
    // Burada, ISession arabirimine bir nesneyi Json formatında saklama özelliği ekleyerek genişlettim.
    // Bu sayede, bir nesneyi Json formatında oturumda saklayabilirim.
    //Ayrıca bu nesneyi GetObjectFromJson methodu ile tekrar orjinal hale getirebilirim.
    public static class SessionExtensions
    {
        // Bu method, belirli bir string (key("Card")) altında saklanan nesneyi Json formatına çevirir.
        public static void SetObjectFromJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        //Bu method, Belirli string (key("Card")) altında saklanan Json formatındaki veriyi tekrar orjinal hale getirir.
        // T? daki "?" işaret T türünün null olabileceğini belirtir. Ayrıca olası Null uyarılarının önüne geçmiş oluyor.
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            //string (key("Card")) altında saklanan nesneyi alır.
            var value = session.GetString(key);
            //Json formatındaki nesneyi orjinal haline çevirir.
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

using System.Text.Json;

namespace KendinInşaEtSonSurumWebApp.Extensions
{
    //Extension(Uzatma sınıflar) class'lar mevcut sınıfa dokunmadan yeni özellikler eklememizi sağlar.
    // this anahtar kelimesi ile belirttiğimiz türe özel genişletmeler yapabiliyoruz.
    // Burada, ISession arabirimine bir nesneyi Json formatında saklama özelliği ekleyerek genişlettim.
    // Bu sayede, bir nesneyi Json formatına çevirerek oturumda saklayabilirim.
    //Ayrıca bu nesneyi tekrar orjinal hale getirebilirim
    public static class SessionExtensions
    {
        public static void SetObjectFromJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // T? daki "?" işaret T türünün null olabileceğini belirtir. Ayrıca olası Null uyarılarının önüne geçmiş oluyor. 
        public static T? GetObjectFromJson<T>(this ISession session, string key) 
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

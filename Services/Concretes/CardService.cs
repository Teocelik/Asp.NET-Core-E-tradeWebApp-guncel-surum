using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.Services.Interfaces;

namespace KendinInşaEtSonSurumWebApp.Services.Concretes
{
    public class CardService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public void AddItemToCard(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ClearCard()
        {
            throw new NotImplementedException();
        }

        public Card GetCard()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}

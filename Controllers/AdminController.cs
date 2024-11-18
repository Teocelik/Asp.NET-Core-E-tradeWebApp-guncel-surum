using Microsoft.AspNetCore.Mvc;

namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

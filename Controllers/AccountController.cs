using KendinInşaEtSonSurumWebApp.Models;
using KendinInşaEtSonSurumWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KendinInşaEtSonSurumWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; //Kullanıcı oluşturma, güncelleme, parola sıfırlama,
                                                         //rol atama gibi işlemleri yapmamızı sağlar.

        private readonly SignInManager<User> _signInManager; //Kullanıcıların giriş işlemlerini yönetir. Giriş yapma,
                                                             //çıkış yapma, iki faktörlü kimlik doğrulama gibi işlemleri içerir.

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]//giriş sayfasını görüntülemek için gerekli olan View'i döner.
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]// giriş sayfasında, kullanıcının bilgilerini alır ve kimlik doğrulama işlemlerini yapar.
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            /*Info: PasswordSignInAsync nedir? : ASP.NET Core Identity kütüphanesinde, 
                    kullanıcı giriş işlemleri için kullanılan bir metoddur. bu yöntem,
                    kullanıcının girdiği kimlik bilgilerini doğrular ve oturum açma işlemlerini gerçekleştirir.

               PasswordSignInAsync ne işe yarar? : kullanıcı giriş bilgilerini doğrular, bilgiler doğruysa oturum açar.
             */

            if (ModelState.IsValid)//gelen veriler doğru formattaysa (ModelState, form verilerini ve doğrulama durumunu tutan bir nesnedir.) 
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);//SingInResult türünde bir sonuç döndürür.

                if (result.Succeeded)//gelen veriler doğruysa..
                {
                    return RedirectToAction("Index", "Home");
                }
                else if(result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Hesap kilitlendi!");
                }
                else if(result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "Giriş izni yok!");
                }
                else if(result.RequiresTwoFactor)
                {
                    ModelState.AddModelError(string.Empty, "İki adımlı doğrulama yapılmalı!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Giriş başarısız!");
                }
            }
            return View(model);
        }

        [HttpGet]//kayıt ol sayfasını görüntülemek için gerekli olan View'i döner.
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)//kullanıcının girdiği bilgilerin formatı doğruysa..
            {
                // kullanıcının girdiği bilgilerle yeni bir User nesnesi oluştur.
                var user = new User { UserName = model.UserName, Email = model.Email };
                // _userManager nesnesini kullanarak yeni bir kullanıcı oluşturma işlemi yapılıyor.
                // CreateAsync methodu ile, oluşturulan kullanıcı veri tabanına kaydediliyor.
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)// kullanıcı kayıt edilme işlemi başarılıysa..
                {
                    // kayıt olan kullanıcı için oturumu açar
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    
    }
}

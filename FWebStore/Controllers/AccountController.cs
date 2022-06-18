using FWebStore.Domain.Entities.Identity;
using FWebStore.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager) //Менеджеры нужны для регистрации нового пользователя
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
        }
        [HttpGet]
        public IActionResult Register() => View(new RegisterUserViewModel()); //действие отправляет пустую ViewModel

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel Model) //действие принимает заполненную ViewModel
        {
            if (!ModelState.IsValid) //проходим валидацию модели
            {
                return View(Model); //если неверна, то возвращаем с ошибками
            }

            var user = new User
            {
                UserName = Model.UserName,

            };

            var registration_result = await _userManager.CreateAsync(user, Model.Password);
            if (registration_result.Succeeded) //если результат регистрации успешен, то производим вход пользователя в систему и переадресуем на главную
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors) //если при регистрации есть ошибки, то сохраняем их в ModelState и отправляем пользователю форму с ошибками
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(Model);
        }

        public IActionResult Login() => View();
        public IActionResult Logout() => RedirectToAction("Index", "Home");
        public IActionResult AccessDenied() => View();
    }
}


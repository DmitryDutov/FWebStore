using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Данные из первого кортроллера");
            return View("Index");
        }
    }
}


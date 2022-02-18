using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class BlogsController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
    }
}


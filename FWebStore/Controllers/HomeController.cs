using FWebStore.Services.Interfaces;
using FWebStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices]IProductData ProductData) //Получаем сервисы прямо в индекс через атрибут
        {
            //var  value = ControllerContext.HttpContext.Request.RouteValues.Values;
            //return Content("Данные из первого кортроллера");
            //return View("Index");

            var products = ProductData.GetProducts()
                .OrderBy(p => p.Order)
                .Take(6)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                });

            ViewBag.Products = products; //Зачем-то передаём через ViewBag, хотя можно и return View(products): в виде модели;

            return View();
        }

        public string ConfiguredAction(string id, string value, string nextVal)
        {
            return $"ConfiguredAction - {id}: {value} + {nextVal}";
        }

        public void Throw(string Message) => throw new ApplicationException(Message); //можем передать сообщение в перехватчик как параметр через адресную строку

        public IActionResult Error404() => View();
    }
}


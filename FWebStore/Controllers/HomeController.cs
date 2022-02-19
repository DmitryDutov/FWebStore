﻿using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var  value = ControllerContext.HttpContext.Request.RouteValues.Values;

            //return Content("Данные из первого кортроллера");
            return View("Index");
        }

        public string ConfiguredAction(string id, string value, string nextVal)
        {
            return $"ConfiguredAction - {id}: {value} + {nextVal}";
        }
    }
}


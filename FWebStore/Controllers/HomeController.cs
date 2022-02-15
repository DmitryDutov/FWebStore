using FWebStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    public class HomeController : Controller
    {
        public static List<Employee> __Employees = new List<Employee>()
        {
            new Employee{Id = 1, LastName = "L1", FirstName = "F1", Age = 34},
            new Employee{Id = 2, LastName = "L2", FirstName = "F2", Age = 35},
            new Employee{Id = 3, LastName = "L3", FirstName = "F2", Age = 36},
        };
        public IActionResult Index()
        {
            //return Content("Данные из первого кортроллера");
            return View("Index");
        }

        public string ConfiguredAction(string id, string value, string nextVal)
        {
            return $"ConfiguredAction - {id}: {value} + {nextVal}";
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }
    }
}


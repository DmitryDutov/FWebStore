using FWebStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    [Route("Empl/{action=Index}/{Id?}")]
    [Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        public static List<Employee> __Employees = new List<Employee>()
        {
            new Employee{Id = 1, LastName = "L1", FirstName = "F1",Patronumic = "P1", Age = 34},
            new Employee{Id = 2, LastName = "L2", FirstName = "F2",Patronumic = "P2", Age = 35},
            new Employee{Id = 3, LastName = "L3", FirstName = "F2",Patronumic = "P3", Age = 36},
        };

        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }

        [Route("~/employees/info-{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = __Employees.FirstOrDefault(x => x.Id == Id);

            if (employee == null)
                return NotFound();
            return View(employee);
        }
    }
}


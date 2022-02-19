using FWebStore.Data;
using FWebStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    //[Route("Empl/{action=Index}/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly ICollection<Employee> __Employees;
        public EmployeesController()
        {
            __Employees = TestData.Employees;
        }
        
        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }

        //[Route("~/employees/info-{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = __Employees.FirstOrDefault(x => x.Id == Id);

            if (employee == null)
                return NotFound();
            return View(employee);
        }
    }
}


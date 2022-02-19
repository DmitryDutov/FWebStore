using FWebStore.Data;
using FWebStore.Models;
using FWebStore.ViewModels;
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

        //public IActionResult Create() => View();
        public IActionResult Edit(int Id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
            {
                NotFound();
            }

            var model = new EmployeeEditViewModel //заполняем VM данными для отправки на форму
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronumic = employee.Patronumic,
                Age = employee.Age,
            };

            return View(model); //данная форма будет отправлена пользователю, после заполения её и нажатия кнопки сформируется POST-запрос
        }

        public IActionResult Edit(EmployeeEditViewModel Model)
        {
            //Обработка VM (которая будет происходить в сервисе)
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id) => View();

    }
}


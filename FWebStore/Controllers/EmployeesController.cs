using FWebStore.Models;
using FWebStore.Services.Interfaces;
using FWebStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    //[Route("Empl/{action=Index}/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData __EmployeesData; //Сохраняем полученный объект IEmployeesData в приватном поле
        //private readonly ICollection<Employee> __Employees; // Изменяем все поля с __Employees на методы интерфейса
        public EmployeesController(IEmployeesData EmployeesData) //Передаём IEmployeesData (внедряем зависимость от сервиса IEmployeesData)
        {
            __EmployeesData = EmployeesData;
            //__Employees = TestData.Employees;
        }
        
        public IActionResult Index()
        {
            //var result = __Employees;
            var result = __EmployeesData.GetAll();
            return View(result);
        }

        //[Route("~/employees/info-{Id}")]
        public IActionResult Details(int Id)
        {
            //var employee = __Employees.FirstOrDefault(x => x.Id == Id);
            var employee = __EmployeesData.GetById(Id);

            if (employee == null)
                return NotFound();
            return View(employee);
        }

        //public IActionResult Create() => View();
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            //var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            var employee = __EmployeesData.GetById(Id);
            if (employee is null) 
                NotFound();

            var model = new EmployeeViewModel //заполняем VM данными для отправки на форму
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronumic = employee.Patronumic,
                Age = employee.Age,
            };

            return View(model); //данная форма будет отправлена пользователю, после заполения её и нажатия кнопки сформируется POST-запрос
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            //Обработка VM (которая будет происходить в сервисе)
            //из ViewModel нужно собрать обратно сотрудника
            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.FirstName,
                Patronumic = Model.Patronumic,
                Age = Model.Age,
            };

            if (! __EmployeesData.Edit(employee))
                return NotFound();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            //Проверка на выход за пределы диапазона
            if (Id < 0)
                return BadRequest();
            //Проверка на null
            var employee = __EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();

            //Заполняем VM данными для отправки на форму
            var model = new EmployeeViewModel //заменить на EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronumic = employee.Patronumic,
                Age = employee.Age,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int Id)
        {
            //__EmployeesData.Delete(Id);
            if (!__EmployeesData.Delete(Id))
                return NotFound();

            return RedirectToAction("Index");
        }
    }
}


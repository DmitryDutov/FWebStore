//using FWebStore.Models;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;
using FWebStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FWebStore.Controllers
{
    //[Route("Empl/{action=Index}/{Id?}")]
    //[Route("Staff/{action=Index}/{Id?}")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData __EmployeesData; //Сохраняем полученный объект IEmployeesData в приватном поле

        private readonly ILogger<EmployeesController> _Logger;

        //private readonly ICollection<Employee> __Employees; // Изменяем все поля с __Employees на методы интерфейса
        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger) //Передаём IEmployeesData (внедряем зависимость от сервиса IEmployeesData)
        {
            __EmployeesData = EmployeesData;
            _Logger = Logger;
            //__Employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            //var result = __Employees;
            var result = __EmployeesData.GetAll()
                //.OrderBy(e => e.Order)
                //.Select(e=> new Employee()
                //{
                //    Id = e.Id,
                //    FirstName = e.FirstName,
                //    LastName = e.LastName,
                //    Age = e.Age,
                //    Patronumic = e.Patronumic,
                //})
                ;
            return View(result);
        }

        //[Route("~/employees/info-{Id}")]
        public IActionResult Details(int Id)
        {
            //var employee = __Employees.FirstOrDefault(x => x.Id == Id);
            var employee = __EmployeesData.GetById((int)Id);
            if (employee is null)
            {
                _Logger.LogWarning("Сотрудник с Id: {0} не был найден для редактирования", employee.Id);
                NotFound();
            }

            var model = new Employee 
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronumic = employee.Patronumic,
                Age = employee.Age,
            };

            _Logger.LogInformation("Сотрудник {0} взят на редактирование", employee);
            return View(model); //данная форма будет отправлена пользователю, после заполения её и нажатия кнопки сформируется POST-запрос
        }

        //Вызвыем метод Edit из метода Create и передаём ему пустую модель
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return View(new EmployeeViewModel());

            //var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            var employee = __EmployeesData.GetById((int)Id);
            if (employee is null)
            {
                _Logger.LogWarning("Сотрудник с Id: {0} не был найден для редактирования", employee.Id);
                NotFound();
            }

            var model = new EmployeeViewModel //заполняем VM данными для отправки на форму
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronumic = employee.Patronumic,
                Age = employee.Age,
            };

            _Logger.LogInformation("Сотрудник {0} взят на редактирование", employee);
            return View(model); //данная форма будет отправлена пользователю, после заполения её и нажатия кнопки сформируется POST-запрос
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            //Для проверок ошибок валидации ипользуем свойство ModelState
            if (!ModelState.IsValid)
            {
                return View(Model);
            }
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

            if (Model.Id == 0)
            {
                __EmployeesData.Add(employee);
                _Logger.LogInformation("Создан новый сотрудник {0}", employee);
            }
            else if (!__EmployeesData.Edit(employee))
            {
                _Logger.LogInformation("Информация о сутруднике {0} изменена", employee);
                return NotFound();
            }

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

            _Logger.LogInformation("Сотрудник с Id: {0} удалён", Id);
            return RedirectToAction("Index");
        }
    }
}


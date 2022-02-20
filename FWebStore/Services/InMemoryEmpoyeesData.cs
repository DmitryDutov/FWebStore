using FWebStore.Data;
using FWebStore.Models;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services
{
    public class InMemoryEmpoyeesData : IEmployeesData
    {
        private readonly ICollection<Employee> _Employees;
        private int _MaxFreeId;
        public InMemoryEmpoyeesData() //в конструктор можно передавать другие сервисы
        {
            _Employees = TestData.Employees;
            _MaxFreeId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 0) + 1; //получам максимальный Id (имитация работы БД)
        }
        public IEnumerable<Employee> GetAll() => _Employees; //возвращаем всех сотрудников

        public Employee? GetById(int id) => _Employees.FirstOrDefault(employee => employee.Id == id); //возвращаем одного сотрудника по Id

        public int Add(Employee employee) //добавляем сотрудника
        {
            if (employee == null) //проверка на null
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) //при работе с БД эот НЕ ТРЕБУЕТСЯ
                return employee.Id;

            employee.Id = _MaxFreeId++;
            _Employees.Add(employee);
            return employee.Id; //возвращаем Id добавленного сотрудника
        }
        public bool Edit(Employee employee)
        {
            if (employee == null) //проверка на null
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) //при работе с БД эот НЕ ТРЕБУЕТСЯ
                return true;

            var db_employee = GetById(employee.Id);
            if (db_employee is null)
                return false;

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronumic = employee.Patronumic;
            db_employee.Age = employee.Age;

            //Когда будет БД: не забыть вызвать SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee == null)
                return false;

            _Employees.Remove(employee);
            return true;
        }
    }
}


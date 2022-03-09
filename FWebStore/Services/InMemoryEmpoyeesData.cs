using FWebStore.Data;
using FWebStore.Models;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services
{
    public class InMemoryEmpoyeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmpoyeesData> _logger;
        private readonly ICollection<Employee> _Employees;
        private int _MaxFreeId;
        public InMemoryEmpoyeesData(ILogger<InMemoryEmpoyeesData> Logger) //ILogger обязательно интерфейс. <InMemoryEmpoyeesData> - это название заголовков в журнале
        {
            _logger = Logger;
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
            {
                _logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id: {0}", employee.Id); //при вызове методов логера не использовать интерполяцию
                return false;
            }

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronumic = employee.Patronumic;
            db_employee.Age = employee.Age;

            //Когда будет БД: не забыть вызвать SaveChanges();
            _logger.LogInformation("Информация о сотруднике с Id: {0} была изменена", employee.Id);
            return true;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee == null)
            {
                _logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id: {0}", employee.Id); //при вызове методов логера не использовать интерполяцию
                return false;
            }

            _Employees.Remove(employee);
            _logger.LogInformation("Сотрудник с Id: {0} был успешно удалён", employee.Id);
            return true;
        }
    }
}


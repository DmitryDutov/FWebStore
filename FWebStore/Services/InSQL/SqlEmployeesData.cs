using FWebStore.DAL.Context;
using FWebStore.Domain.Entities;
using FWebStore.Services.Interfaces;

namespace FWebStore.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly ILogger<SqlEmployeesData> _logger;
        private readonly FWebStoreDB _db;

        public SqlEmployeesData(ILogger<SqlEmployeesData> logger, FWebStoreDB db)
        {
            _logger = logger;
            _db = db;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees;
        }

        public Employee? GetById(int id)
        {
            return _db.Employees.FirstOrDefault(employee => employee.Id == id);
        }

        public int Add(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException(nameof(employee));

            if (_db.Employees.Contains(employee))
                return employee.Id;

            _db.Employees.Add(employee);
            return employee.Id;
        }

        public bool Edit(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException(nameof(employee));

            if (_db.Employees.Contains(employee))
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

            _db.Employees.Remove(employee);
            _logger.LogInformation("Сотрудник с Id: {0} был успешно удалён", employee.Id);
            return true;
        }
    }
}

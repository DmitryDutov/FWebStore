using FWebStore.Models;

namespace FWebStore.Services.Interfaces
{
    /// <summary>
    /// Все интерфейсы должны иметь документацию
    /// </summary>
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();
        Employee? Get(int id);
        int Add(Employee employee);
        bool Edit(Employee employee);
        bool Delete(int id);
    }
}


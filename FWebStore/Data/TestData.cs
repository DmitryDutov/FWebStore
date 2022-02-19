using FWebStore.Models;

namespace FWebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>()
        {
            new Employee{Id = 1, LastName = "L1", FirstName = "F1",Patronumic = "P1", Age = 34},
            new Employee{Id = 2, LastName = "L2", FirstName = "F2",Patronumic = "P2", Age = 35},
            new Employee{Id = 3, LastName = "L3", FirstName = "F2",Patronumic = "P3", Age = 36},
        };

    }
}


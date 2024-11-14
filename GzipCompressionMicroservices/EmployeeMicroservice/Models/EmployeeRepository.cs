﻿namespace EmployeeMicroservice.Models
{
    public class EmployeeRepository
    {

        private readonly List<Employee> _employees;

        public EmployeeRepository()
        {
            // Sample employee data
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Department = "HR", Salary = 55000 },
                new Employee { Id = 2, Name = "Jane Smith", Department = "IT", Salary = 75000 }
            };
        }

        public IEnumerable<Employee> GetAll() => _employees;

        public Employee GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public void Add(Employee employee) => _employees.Add(employee);

        public void Update(Employee employee)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.Department = employee.Department;
                existing.Salary = employee.Salary;
            }
        }

        public void Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

    }
}

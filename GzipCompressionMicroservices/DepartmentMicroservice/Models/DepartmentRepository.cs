namespace DepartmentMicroservice.Models
{
    public class DepartmentRepository
    {

        private readonly List<Department> _departments;

        public DepartmentRepository()
        {
            _departments = new List<Department>
            {
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" },
                new Department { Id = 3, Name = "Finance" }
            };
        }

        public IEnumerable<Department> GetAll() => _departments;

        public Department GetById(int id) => _departments.FirstOrDefault(d => d.Id == id);



    }
}

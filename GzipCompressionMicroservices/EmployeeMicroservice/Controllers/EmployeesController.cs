using DepartmentMicroservice.Models;
using EmployeeMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _repository = new EmployeeRepository();
            _httpClientFactory = httpClientFactory;
        }

        // Retrieve all employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // Retrieve a single employee by ID
        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        // Retrieve a single employee by ID and fetch department details from Department Service
        [HttpGet("{id}/details")]
        public async Task<ActionResult> GetEmployeeWithDepartmentDetails(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();

            // Call Department Service for department details
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");  // Request Gzip compressed data
            var departmentResponse = await client.GetAsync($"http://localhost:5001/api/departments/{employee.DepartmentId}");

            if (!departmentResponse.IsSuccessStatusCode)
                return StatusCode((int)departmentResponse.StatusCode);

            var departmentJson = await departmentResponse.Content.ReadAsStringAsync();
            var department = JsonConvert.DeserializeObject<Department>(departmentJson);

            // Combine Employee and Department data
            var result = new
            {
                employee.Id,
                employee.Name,
                employee.Salary,
                Department = department.Name
            };

            return Ok(result);
        }

        // Add a new employee
        [HttpPost]
        public ActionResult Add([FromBody] Employee employee)
        {
            _repository.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // Update an employee
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Employee employee)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();
            employee.Id = id;
            _repository.Update(employee);
            return NoContent();
        }

        // Delete an employee
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();
            _repository.Delete(id);
            return NoContent();
        }
    }

}
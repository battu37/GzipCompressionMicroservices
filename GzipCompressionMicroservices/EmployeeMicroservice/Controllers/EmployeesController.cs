using EmployeeMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMicroservice.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeesController : ControllerBase
        {
            private readonly EmployeeRepository _repository;

            public EmployeesController()
            {
                _repository = new EmployeeRepository();
            }

            [HttpGet]
            public ActionResult<IEnumerable<Employee>> GetAll()
            {
                return Ok(_repository.GetAll());
            }

            [HttpGet("{id}")]
            public ActionResult<Employee> GetById(int id)
            {
                var employee = _repository.GetById(id);
                if (employee == null) return NotFound();
                return Ok(employee);
            }

            [HttpPost]
            public ActionResult Add([FromBody] Employee employee)
            {
                _repository.Add(employee);
                return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
            }

            [HttpPut("{id}")]
            public ActionResult Update(int id, [FromBody] Employee employee)
            {
                var existing = _repository.GetById(id);
                if (existing == null) return NotFound();
                employee.Id = id;
                _repository.Update(employee);
                return NoContent();
            }

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
using DepartmentMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly DepartmentRepository _repository;

        public DepartmentsController()
        {
            _repository = new DepartmentRepository();
        }


        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetById(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return Ok(department);
        }


    }
}

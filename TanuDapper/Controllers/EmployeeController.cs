using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanuDapper.Interface;
using TanuDapper.Model;

namespace TanuDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository)); ;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _employeeRepository.GetEmployee();
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("AddEmployees")]
        public async Task<IActionResult> Add(EmployeeModel employeeModel)
        {
            var data = await _employeeRepository.CreateEmployee(employeeModel);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut("UpdateEmployees")]
        public async Task<IActionResult> Update(int id,EmployeeModel employeeModel)
        {
            var data = await _employeeRepository.UpdateEmployee(id,employeeModel);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpDelete("DeleteEmployees")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _employeeRepository.DeleteEmployee(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetEmployeeByName")]
        public async Task<IActionResult> GetEmployeeByName(string FirstName)
        {
            var data = await _employeeRepository.GetByName(FirstName);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

    }
}

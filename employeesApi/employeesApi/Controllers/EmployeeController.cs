using employeesApi.DTO;
using employeesApi.Interfaces;
using employeesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employeesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetEmployees))]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetEmployeeById))]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employeeByID = await _employeeRepository.GetEmployeeById(id);
            if( employeeByID == null)
            {
                return NotFound();
            }

            return Ok(employeeByID);
        }

        [HttpPost]
        [ActionName(nameof(AddEmployee))]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Cedula = createEmployeeDto.Cedula,
                Nombre = createEmployeeDto.Nombre,
                Foto = createEmployeeDto.Foto,
                FechaIngreso = createEmployeeDto.FechaIngreso,
                JobTitleId = createEmployeeDto.JobTitleId
            };

            await _employeeRepository.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateEmployee))]
        public async Task<ActionResult> UpdateEmployee(int id, Employee employee)
        {
            if( id != employee.Id )
            {
                return BadRequest();
            }

            await _employeeRepository.UpdateEmployee(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteEmployee))]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            if( employee == null )
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }

        [HttpPost("upload") ]
        [ActionName(nameof(UploadImgEmployee))]
        public async Task<ActionResult> UploadImgEmployee(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var imgByte = await _employeeRepository.UploadImgEmployee(file);

                    return Ok(imgByte);
                }

                return BadRequest("No se proporcionó ninguna imagen.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar la imagen: {ex.Message}");
            }
        }


    }
}

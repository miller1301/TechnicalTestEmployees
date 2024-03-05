using employeesApi.DTO;
using employeesApi.Models;

namespace employeesApi.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<EmployeeDTO?> GetEmployeeById(int id);
        IEnumerable<EmployeeDTO> GetAllEmployees();
        Task<bool> UpdateEmployee(Employee employee);
        Task<byte[]> UploadImgEmployee(IFormFile file);
    }
}

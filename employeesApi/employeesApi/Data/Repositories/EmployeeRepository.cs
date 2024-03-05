using employeesApi.Context;
using employeesApi.DTO;
using employeesApi.Interfaces;
using employeesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employeesApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            return _context.Employees
                .Include(e => e.JobTitle).Select(x => new EmployeeDTO()
                {
                    Id = x.Id,
                    Cedula = x.Cedula,
                    FechaIngreso = x.FechaIngreso,
                    Foto = x.Foto,
                    JobTitleId = x.JobTitleId,
                    Nombre = x.Nombre,
                    JobTitleDTO = new JobTitleDTO
                    {
                        Id = x.JobTitle.Id,
                        Nombre = x.JobTitle.Nombre
                    }
                });
        }

        public async Task<EmployeeDTO?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.JobTitle)
                .Select(x => new EmployeeDTO()
                {
                    Id = x.Id,
                    Cedula = x.Cedula,
                    FechaIngreso = x.FechaIngreso,
                    Foto = x.Foto,
                    JobTitleId = x.JobTitleId,
                    Nombre = x.Nombre,
                    JobTitleDTO = new JobTitleDTO
                    {
                        Id = x.JobTitle.Id,
                        Nombre = x.JobTitle.Nombre
                    }
                })
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return employee;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);

            if (employee is null)
                return false;

            _context.Set<Employee>().Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<byte[]> UploadImgEmployee(IFormFile file)
        {
            byte[] imageBytes;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            return imageBytes;
        }
    }
}

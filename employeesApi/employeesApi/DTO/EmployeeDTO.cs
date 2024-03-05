using employeesApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace employeesApi.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public required int Cedula { get; set; }
        public required string Nombre { get; set; }
        public required byte[] Foto { get; set; }
        public required DateTime FechaIngreso { get; set; }
        public required int JobTitleId { get; set; }
        public JobTitleDTO JobTitleDTO { get; set; }
    }

    public class JobTitleDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }

    public class CreateEmployeeDto
    {
        public required int Cedula { get; set; }
        public required string Nombre { get; set; }
        public required byte[] Foto { get; set; }
        public required DateTime FechaIngreso { get; set; }
        public required int JobTitleId { get; set; }
    }
}

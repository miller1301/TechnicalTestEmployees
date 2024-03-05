using System.ComponentModel.DataAnnotations.Schema;

namespace employeesApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required int Cedula { get; set; }
        public required string Nombre { get; set; }
        public required byte[] Foto { get; set; }
        public required DateTime FechaIngreso { get; set; }
        public required int JobTitleId { get; set; }
        [ForeignKey("JobTitleId")]
        public virtual JobTitle? JobTitle { get; set; }
    }
}

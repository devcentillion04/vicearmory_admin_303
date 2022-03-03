using System.ComponentModel.DataAnnotations;

namespace ViceArmory.DTO.RequestObject.Employee
{
    public class EmployeeLoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

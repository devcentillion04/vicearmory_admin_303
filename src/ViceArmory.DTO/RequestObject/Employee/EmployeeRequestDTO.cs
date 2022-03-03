using System;

namespace ViceArmory.DTO.RequestObject.Employee
{
    public class EmployeeRequestDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

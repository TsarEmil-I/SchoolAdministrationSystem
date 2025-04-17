using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.DTOs
{
    public class ImportStudentDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }
    }
}

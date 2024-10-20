using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public int LeftAbsenceDays { get; set; } // Starting with 15 days for a year

        public List<Absence> Absences { get; set; } = new List<Absence>();
    }
}

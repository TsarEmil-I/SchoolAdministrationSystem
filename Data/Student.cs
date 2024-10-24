using Microsoft.AspNetCore.Razor.TagHelpers;
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

        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public int LeftAbsenceDays { get; set; } = 15;

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();

        public virtual List<Class> Classes { get; set; } = new List<Class>();
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Student : BaseEntity
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Second Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
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

        private int age;
        public int Age
        {
            get
            {
               return age;
            }

            set
            {
                if (value > 0)
                {
                    age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Age cannot be negative!");
                }
            }
        }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

        public int LeftAbsenceDays { get; set; } = 15;

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();
    }
}

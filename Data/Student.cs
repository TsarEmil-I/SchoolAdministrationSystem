using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Student : BaseEntity
    {
        [Required]
        [DisplayName("First Name")]
        [RegularExpression(@"^[А-Яа-яЁё]+$", ErrorMessage = "First Name must contain only Cyrillic letters.")]
        public string FirstName { get; set; }

        [DisplayName("Second Name")]
        [RegularExpression(@"^[А-Яа-яЁё]+$", ErrorMessage = "Middle Name must contain only Cyrillic letters.")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[А-Яа-яЁё]+$", ErrorMessage = "Last Name must contain only Cyrillic letters.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        [Required]
        public Gender Gender { get; set; }

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
        [RegularExpression(@"^\+[0-9]{1,3}[0-9]{8,12}$", ErrorMessage = "Phone number must be in international format (e.g., +359xxxxxxxx).")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

        public int LeftAbsenceDays { get; set; } = 15;

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();
    }
}

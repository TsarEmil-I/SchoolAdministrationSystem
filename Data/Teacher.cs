using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data
{
    public class Teacher : BaseEntity
    {
        [Required]
        [DisplayName("First Name")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "First Name must contain only Cyrillic letters and dashes.")]
        public string FirstName { get; set; }

        [DisplayName("Second Name")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Middle Name must contain only Cyrillic letters and dashes.")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Last Name must contain only Cyrillic letters and dashes.")]
        public string LastName { get; set; }

        [DisplayName("Head Teacher")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        public virtual Class? Class { get; set; }
    }
}

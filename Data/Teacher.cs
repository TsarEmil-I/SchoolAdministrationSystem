using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data
{
    public class Teacher : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [ForeignKey("Class")]
        public string Number { get; set; }

    }
}

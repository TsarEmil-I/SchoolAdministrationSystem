using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Class : BaseEntity
    {
        [Required]
        public string Speciality { get; set; }

        [DisplayName("Head Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        public virtual List<Student>? Students { get; set; } = new List<Student>();

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();

    }
}

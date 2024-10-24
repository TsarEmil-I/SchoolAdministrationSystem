using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Class : BaseEntity
    {
        public string Speciality { get; set; }

        public virtual List<Student>? Students { get; set; } = new List<Student>();

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.Data
{
    public class Class : BaseEntity
    {
        [Required]
        [DisplayName("Клас")]
        public string Speciality { get; set; }

        [DisplayName("Класен ръководител")]
        public int TeacherId { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        public virtual List<Student>? Students { get; set; } = new List<Student>();

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();

    }
}

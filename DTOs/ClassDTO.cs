using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs
{
    public class ClassDTO : BaseDTO
    {
        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Клас")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Класен ръководител")]
        public int TeacherId { get; set; }
        [JsonIgnore]
        public TeacherDTO? Teacher { get; set; }

        [JsonIgnore]
        public List<StudentDTO>? Students { get; set; }

        [JsonIgnore]
        public List<AbsenceDTO>? Absences { get; set; }
    }
}

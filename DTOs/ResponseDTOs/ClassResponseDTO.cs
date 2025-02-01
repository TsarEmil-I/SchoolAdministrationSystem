using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class ClassResponseDTO : BaseResponseDTO
    {
        [DisplayName("Клас")]
        public string Speciality { get; set; }

        public int TeacherId { get; set; }
        [JsonIgnore]
        [DisplayName("Класен ръководител")]
        public TeacherResponseDTO Teacher { get; set; }

        [JsonIgnore]
        public List<StudentResponseDTO>? Students { get; set; }

        [JsonIgnore]
        public List<AbsenceResponseDTO>? Absences { get; set; }
    }
}

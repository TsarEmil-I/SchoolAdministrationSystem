using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class ClassResponseDTO : BaseResponseDTO
    {
        public string Speciality { get; set; }

        public int TeacherId { get; set; }
        [JsonIgnore]
        public TeacherResponseDTO Teacher { get; set; }

        public List<StudentResponseDTO>? Students { get; set; }

        public List<AbsenceResponseDTO>? Absences { get; set; }
    }
}

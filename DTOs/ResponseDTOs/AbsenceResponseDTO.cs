using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class AbsenceResponseDTO : BaseResponseDTO
    {
        public string? SequenceNumber { get; set; }

        public string Reason { get; set; }

        public DateOnly Start { get; set; }

        public DateOnly End { get; set; }

        public int ClassId { get; set; }
        [JsonIgnore]
        public ClassResponseDTO Class { get; set; }
        public int StudentId { get; set; }
        [JsonIgnore]
        public StudentResponseDTO Student { get; set; }
        public int Days { get; set; }
    }
}

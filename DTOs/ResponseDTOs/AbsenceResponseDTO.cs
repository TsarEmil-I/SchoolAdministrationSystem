using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class AbsenceResponseDTO : BaseResponseDTO
    {
        [DisplayName("Входящ номер")]
        public string? SequenceNumber { get; set; }

        [DisplayName("Причина")]
        public string Reason { get; set; }

        [DisplayName("От")]
        public DateOnly Start { get; set; }
        [DisplayName("До")]
        public DateOnly End { get; set; }

        public int ClassId { get; set; }
        [JsonIgnore]
        [DisplayName("Клас")]
        public ClassResponseDTO Class { get; set; }
        public int StudentId { get; set; }
        [JsonIgnore]
        [DisplayName("Ученик")]
        public StudentResponseDTO Student { get; set; }
        public int Days { get; set; }
    }
}

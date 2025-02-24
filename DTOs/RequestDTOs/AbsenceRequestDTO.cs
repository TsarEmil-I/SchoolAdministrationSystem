using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class AbsenceRequestDTO : BaseRequestDTO
    {
        public string? SequenceNumber { get; set; } // Not a primary key, just a tracking number for the school administration!

        [Required(ErrorMessage = "Това поле е задължително")]
        [MaxLength(100)]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public DateOnly Start { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public DateOnly End { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        public int StudentId { get; set; }

    }
}

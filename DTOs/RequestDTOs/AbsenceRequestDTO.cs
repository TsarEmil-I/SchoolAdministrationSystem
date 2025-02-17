using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class AbsenceRequestDTO : BaseRequestDTO
    {
        public string? SequenceNumber { get; set; } // Not a primary key, just a tracking number for the school administration!

        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }

        [Required]
        public DateOnly Start { get; set; }

        [Required]
        public DateOnly End { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int StudentId { get; set; }

    }
}

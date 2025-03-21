using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class Absence : BaseEntity
    {
        [DisplayName("Входящ номер")]
        public string? SequenceNumber { get; set; } // Not a primary key, just a tracking number for the school administration!
        [Required]
        [MaxLength(100)]
        [DisplayName("Причина")]
        public string Reason { get; set; }
        [Required]
        [DisplayName("От дата")]
        public DateOnly Start { get; set; }
        [Required]
        [DisplayName("До дата")]
        public DateOnly End { get; set; }

        [Required]
        public int ClassId { get; set; }
        [DisplayName("Клас")]
        public virtual Class? Class { get; set; }

        [Required]
        public int StudentId { get; set; }
        [DisplayName("Ученик")]
        public virtual Student? Student { get; set; }

        public int Days { get; set; }

    }
}

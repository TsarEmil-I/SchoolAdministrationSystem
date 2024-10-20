using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class Absence : BaseEntity
    {
        [Required]
        public string SequenceNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

        public Student Student { get; set; }

    }
}

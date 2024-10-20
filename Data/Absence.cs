using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data
{
    public class Absence : BaseEntity
    {
        [Required]
        public string SequenceNumber { get; set; } // Not a primary key, just a tracking number for the school administration!
        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}

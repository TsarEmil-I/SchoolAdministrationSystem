using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data
{
    public class Absence : BaseEntity
    {
        [DisplayName("Sequence Number")]
        public string? SequenceNumber { get; set; } // Not a primary key, just a tracking number for the school administration!
        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }
        [Required]
        public DateOnly Start { get; set; }
        [Required]
        public DateOnly End { get; set; }

        [Required]
        [DisplayName("From class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

        [Required]
        [DisplayName("Student")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int Days
        {
            get
            {
                return DaysBetween(Start, End);
            }
        }

        int DaysBetween(DateOnly d1, DateOnly d2)
        { 
            DateTime dt1 = d1.ToDateTime(TimeOnly.MinValue);
            DateTime dt2 = d2.ToDateTime(TimeOnly.MinValue);

            TimeSpan span = dt2.Subtract(dt1);
            return (int)span.TotalDays;
        }
    }
}

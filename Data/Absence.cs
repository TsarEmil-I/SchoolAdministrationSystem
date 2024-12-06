using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdministrationSystem.Data
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

            //if (dt1 > dt2)
            //{
            //    // Swap dates if the start date is later than the end date
            //    (dt1, dt2) = (dt2, dt1);
            //}

            int weekdayCount = 0;

            for (DateTime current = dt1; current <= dt2; current = current.AddDays(1))
            {
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdayCount++;
                }
            }

            return weekdayCount;
        }

    }
}

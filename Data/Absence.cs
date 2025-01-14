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

            int weekdayCount = 0;

            //if (dt1 > dt2 || dt1 < DateTime.Today)
            //{
            //    throw new ValidationException("Не може отсъствието да бъде въведено преди днешна дата или началната дата да е по-голяма от крайната!");
            //}

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

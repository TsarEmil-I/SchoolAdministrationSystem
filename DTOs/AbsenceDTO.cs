using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs
{
    public class AbsenceDTO : BaseDTO
    {
        [DisplayName("Входящ номер")]
        public string? SequenceNumber { get; set; }

        [DisplayName("Причина")]
        public string Reason { get; set; }

        [DisplayName("От")]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyyг.}", ApplyFormatInEditMode = true)]
        public DateOnly Start { get; set; }
        [DisplayName("До")]

        [DisplayFormat(DataFormatString = "{0:d MMMM yyyyг.}", ApplyFormatInEditMode = true)]
        public DateOnly End { get; set; }

        public int ClassId { get; set; }
        [JsonIgnore]
        [DisplayName("Клас")]
        public virtual ClassDTO? Class { get; set; }
        public int StudentId { get; set; }
        [JsonIgnore]
        [DisplayName("Ученик")]
        public virtual StudentDTO? Student { get; set; }
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

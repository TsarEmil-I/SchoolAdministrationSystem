using SchoolAdministrationSystem.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs
{
    public class AbsenceDTO : BaseDTO
    {      
        [DisplayName("Входящ номер")]
        public string? SequenceNumber { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [DisplayName("Причина")]
        public string Reason { get; set; }

        [DisplayName("От")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyyг.}", ApplyFormatInEditMode = true)]
        public DateOnly Start { get; set; }

        [DisplayName("До")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyyг.}", ApplyFormatInEditMode = true)]
        public DateOnly End { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [JsonIgnore]
        public int ClassId { get; set; }

        [DisplayName("Клас")]
        public virtual ClassDTO? Class { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [JsonIgnore]
        public int StudentId { get; set; }

        [DisplayName("Ученик")]
        public virtual StudentDTO? Student { get; set; }
        public int Days { get; set; }
    }
}

using SchoolAdministrationSystem.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs
{
    public class StudentDTO : BaseDTO
    {
        [DisplayName("Име")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [DisplayName("Презиме")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Фамилията трябва да съдържа само букви, без специални знаци и цифри.")]
        public string LastName { get; set; }

        [DisplayName("Име")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        [DisplayName("Пол")]
        [Required(ErrorMessage = "Това поле е задължително")]
        public Gender Gender { get; set; }

        [DisplayName("Възраст")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [Range(6, 99, ErrorMessage = "Възрастта трябва да бъде положително число: 6-99!")]
        public int Age { get; set; }

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Това поле е задължително")]
        public string Address { get; set; }

        [DisplayName("Телефонен номер")]
        [Required(ErrorMessage = "Това поле е задължително")]
        [RegularExpression(@"^\+[0-9]{1,3}[0-9]{8,12}$", ErrorMessage = "Телефонният номер трябва да е в международен формат (напр., +359xxxxxxxx).")]
        public string PhoneNumber { get; set; }

        [DisplayName("Клас")]
        [Required(ErrorMessage = "Това поле е задължително")]
        public int ClassId { get; set; }
        public ClassDTO? Class { get; set; }
        [DisplayName("Оставащи дни")]
        public int LeftAbsenceDays
        {
            get
            {
                return 15 - (Absences?.Sum(a => a.Days) ?? 0);
            }
        }

        [JsonIgnore]
        public virtual List<AbsenceDTO>? Absences { get; set; } = new List<AbsenceDTO>();
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class Student : BaseEntity
    {
        [Required]
        [DisplayName("Име")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [DisplayName("Презиме")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Фамилия")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Фамилията трябва да съдържа само букви, без специални знаци и цифри.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        [Required]
        [DisplayName("Пол")]
        public Gender Gender { get; set; }

        [Required]
        private int age;
        [DisplayName("Възраст")]
        [System.ComponentModel.DataAnnotations.Range(6, 99, ErrorMessage = "Възрастта трябва да бъде положително число: 6-99!")]
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        [Required]
        [DisplayName("Местожителство")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Телефон")]
        [RegularExpression(@"^\+[0-9]{1,3}[0-9]{8,12}$", ErrorMessage = "Телефонният номер трябва да е в международен формат (напр., +359xxxxxxxx).")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Клас")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }

        private const int TotalStartingAbsenceDays = 15;

        [DisplayName("Оставащи дни")]
        public int LeftAbsenceDays
        {
            get
            {
                return TotalStartingAbsenceDays - (Absences?.Sum(a => a.Days) ?? 0);
            }
        }

        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();
    }
}

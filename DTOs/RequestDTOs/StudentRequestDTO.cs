using SchoolAdministrationSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class StudentRequestDTO : BaseRequestDTO
    {
        [Required]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Фамилията трябва да съдържа само букви, без специални знаци и цифри.")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Range(6, 99, ErrorMessage = "Възрастта трябва да бъде положително число: 6-99!")]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\+[0-9]{1,3}[0-9]{8,12}$", ErrorMessage = "Телефонният номер трябва да е в международен формат (напр., +359xxxxxxxx).")]
        public string PhoneNumber { get; set; }

        [Required]
        public int ClassId { get; set; }
    }
}

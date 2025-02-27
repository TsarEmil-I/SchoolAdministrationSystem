using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs
{
    public class TeacherDTO : BaseDTO
    {
        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Име")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Фамилията трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string LastName { get; set; }

        [DisplayName("Класен ръководител")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }

        }
        public int ClassId { get; set; }

        public ClassDTO? Class { get; set; }

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class Teacher : BaseEntity
    {
        public string? UserId { get; set; }

        [Required]
        [DisplayName("Име")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [DisplayName("Презиме")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Фамилия")]
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

        [JsonIgnore]
        public virtual Class? Class { get; set; }
    }
}

using SchoolAdministrationSystem.Data.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class StudentResponseDTO : BaseResponseDTO
    {
        [DisplayName("Име")]
        public string FirstName { get; set; }

        [DisplayName("Презиме")]
        public string MiddleName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        [DisplayName("Пол")]
        public Gender Gender { get; set; }
        [DisplayName("Възраст")]
        public int Age { get; set; }
        [DisplayName("Адрес")]
        public string Address { get; set; }
        [DisplayName("Телефонен номер")]
        public string PhoneNumber { get; set; }
        public int ClassId { get; set; }
        [DisplayName("Клас")]
        public ClassResponseDTO Class { get; set; }
        [DisplayName("Оставащи дни")]
        public int LeftAbsenceDays
        {
            get
            {
                return 15 - (Absences?.Sum(a => a.Days) ?? 0);
            }
        }

        [JsonIgnore]
        public virtual List<Absence>? Absences { get; set; } = new List<Absence>();

    }
}

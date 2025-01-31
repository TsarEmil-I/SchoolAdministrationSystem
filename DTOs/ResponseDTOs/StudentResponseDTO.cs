using SchoolAdministrationSystem.Data.Entities;
using System.Text.Json.Serialization;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class StudentResponseDTO : BaseResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int ClassId { get; set; }
        public ClassResponseDTO Class { get; set; }
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

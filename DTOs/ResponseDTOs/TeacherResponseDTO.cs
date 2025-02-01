using System.ComponentModel;

namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class TeacherResponseDTO : BaseResponseDTO
    {
        [DisplayName("Име")]
        public string FirstName { get; set; }

        [DisplayName("Презиме")]
        public string? MiddleName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Класен ръководител")]
        public string FullName { get; set; }

        public int ClassId { get; set; }
    }
}

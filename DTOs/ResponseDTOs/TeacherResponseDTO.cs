namespace SchoolAdministrationSystem.DTOs.ResponseDTOs
{
    public class TeacherResponseDTO : BaseResponseDTO
    {
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int ClassId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class ClassRequestDTO : BaseRequestDTO
    {
        [Required]
        [Display(Name = "Клас")]
        public string Speciality { get; set; }

        [Required]
        [Display(Name = "Класен ръководител")]
        public int TeacherId { get; set; }
    }
}

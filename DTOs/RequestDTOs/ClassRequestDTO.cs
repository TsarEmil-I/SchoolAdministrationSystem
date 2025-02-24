using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class ClassRequestDTO : BaseRequestDTO
    {
        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Клас")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "Това поле е задължително")]
        [Display(Name = "Класен ръководител")]
        public int TeacherId { get; set; }
    }
}

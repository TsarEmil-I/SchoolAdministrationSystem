using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class Holiday : BaseEntity
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string? Description { get; set; }
    }
}

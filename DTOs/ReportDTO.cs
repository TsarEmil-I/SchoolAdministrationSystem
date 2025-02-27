namespace SchoolAdministrationSystem.DTOs
{
    public class ReportDTO : BaseDTO
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Това поле е задължително")]
        public string ReportType { get; set; }
        public int? StudentId { get; set; }
        public int? ClassId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}

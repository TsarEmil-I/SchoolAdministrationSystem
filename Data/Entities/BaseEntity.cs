using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

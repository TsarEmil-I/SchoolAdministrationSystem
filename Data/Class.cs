namespace SchoolAdministrationSystem.Data
{
    public class Class : BaseEntity
    {
        public string Number { get; set; }
        public string Speciality { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        public List<Absence> Absences { get; set; } = new List<Absence>();

    }
}

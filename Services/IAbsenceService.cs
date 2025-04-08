using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IAbsenceService
    {
        public Task<IEnumerable<AbsenceDTO>> GetAllAbsencesAsync();
        public Task<List<AbsenceDTO>> GetAbsencesByStudentIdAsync(int studentId);
        public Task<List<AbsenceDTO>> GetAbsencesByClassIdAsync(int classId);
        public Task<List<AbsenceDTO>> GetAbsencesByClassIdPeriodAsync(int classId, DateTime start, DateTime end);
        public Task<AbsenceDTO> GetAbsenceByIdAsync(int id);
        public Task<AbsenceDTO> CreateAbsenceAsync(AbsenceDTO absenceDto);
        public Task<AbsenceDTO> UpdateAbsenceAsync(int id, AbsenceDTO absenceDto);
        public Task<bool> DeleteAbsenceAsync(int id);

    }
}

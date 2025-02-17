using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Services
{
    public interface IAbsenceService
    {
        public Task<IEnumerable<AbsenceResponseDTO>> GetAllAbsencesAsync();
        public Task<AbsenceResponseDTO> GetAbsenceByIdAsync(int id);

        public Task<AbsenceResponseDTO> CreateAbsenceAsync(AbsenceRequestDTO absenceDto);

        public Task<AbsenceResponseDTO> UpdateAbsenceAsync(int id, AbsenceRequestDTO absenceDto);

        public Task<bool> DeleteAbsenceAsync(int id);

    }
}

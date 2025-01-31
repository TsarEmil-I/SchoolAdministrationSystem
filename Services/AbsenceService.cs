using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

public class AbsenceService
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IMapper _mapper;

    public AbsenceService(IAbsenceRepository absenceRepository, IStudentRepository studentRepository, IClassRepository classRepository, IMapper mapper)
    {
        _absenceRepository = absenceRepository;
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AbsenceResponseDTO>> GetAllAbsencesAsync()
    {
        var absences = await _absenceRepository.GetAllAbsencesAsync();
        return _mapper.Map<IEnumerable<AbsenceResponseDTO>>(absences);
    }

    public async Task<AbsenceResponseDTO> GetAbsenceByIdAsync(int id)
    {
        var absence = await _absenceRepository.GetAbsenceByIdAsync(id);
        return absence == null ? null : _mapper.Map<AbsenceResponseDTO>(absence);
    }

    public async Task<AbsenceResponseDTO> CreateAbsenceAsync(AbsenceRequestDTO absenceDto)
    {
        var absence = _mapper.Map<Absence>(absenceDto);

        absence.Student = await _studentRepository.GetStudentByIdAsync(absence.StudentId);
        absence.Class = await _classRepository.GetClassByIdAsync(absence.ClassId);

        if (absence.Student == null || absence.Class == null)
        {
            throw new Exception("Невалиден ученик или клас!");
        }

        int absenceDays = absence.Days;

        if (absence.Student.LeftAbsenceDays <= 0 || absence.Student.LeftAbsenceDays < absenceDays)
        {
            throw new Exception("Ученикът няма достатъчно оставащи дни!");
        }

        await _absenceRepository.CreateAbsenceAsync(absence);

        absence.SequenceNumber = GenerateSequenceNumber(absence);
        await _absenceRepository.UpdateAbsenceAsync(absence);

        return _mapper.Map<AbsenceResponseDTO>(absence);
    }

    public async Task<AbsenceResponseDTO> UpdateAbsenceAsync(int id, AbsenceRequestDTO absenceDto)
    {
        var existingAbsence = await _absenceRepository.GetAbsenceByIdAsync(id);
        if (existingAbsence == null)
        {
            return null;
        }

        _mapper.Map(absenceDto, existingAbsence);
        await _absenceRepository.UpdateAbsenceAsync(existingAbsence);

        existingAbsence.SequenceNumber = GenerateSequenceNumber(existingAbsence);
        await _absenceRepository.UpdateAbsenceAsync(existingAbsence);

        return _mapper.Map<AbsenceResponseDTO>(existingAbsence);
    }

    public async Task<bool> DeleteAbsenceAsync(int id)
    {
        var absence = await _absenceRepository.GetAbsenceByIdAsync(id);
        if (absence == null)
        {
            return false;
        }

        await _absenceRepository.DeleteAbsenceAsync(id);
        return true;
    }

    private string GenerateSequenceNumber(Absence absence)
    {
        var classEntity = absence.Class;
        if (classEntity == null)
        {
            throw new Exception("Invalid class selected");
        }

        var classCode = absence.ClassId;
        var studentId = absence.StudentId.ToString("D2");
        var leftAbsenceDays = absence.Student.LeftAbsenceDays.ToString("D2");
        var currentDate = DateTime.Now.ToString("dd.MM.yy");

        return $"{classCode}-{studentId}-{leftAbsenceDays}/{currentDate}";
    }
}

using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

public class AbsenceService : IAbsenceService
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

    public async Task<IEnumerable<AbsenceDTO>> GetAllAbsencesAsync()
    {
        var absences = await _absenceRepository.GetAllAbsencesAsync();
        return _mapper.Map<IEnumerable<AbsenceDTO>>(absences);
    }

    public async Task<AbsenceDTO> GetAbsenceByIdAsync(int id)
    {
        var absence = await _absenceRepository.GetAbsenceByIdAsync(id);
        return absence == null ? null : _mapper.Map<AbsenceDTO>(absence);
    }

    public async Task<AbsenceDTO> CreateAbsenceAsync(AbsenceDTO absenceDto)
    {
        var absence = _mapper.Map<Absence>(absenceDto);

        absence.Student = await _studentRepository.GetStudentByIdAsync(absence.StudentId);
        absence.Class = await _classRepository.GetClassByIdAsync(absence.ClassId);

        if (absence.Student == null || absence.Class == null)
        {
            throw new Exception("Невалиден ученик или клас!");
        }

        if (absence.Start > absence.End) 
        {
            throw new ArgumentException("Не може отсъствието да бъде въведено преди днешна дата!");
        }

        if (absence.Start.ToDateTime(TimeOnly.MinValue) < DateTime.Today)
        {
            throw new ArgumentException("Не може началната дата да е по-голяма от крайната!");
        }

        int absenceDays = absence.Days;
        string leftDays = absence.Student.LeftAbsenceDays.ToString();

        if (absence.Student.LeftAbsenceDays <= 0 || absence.Student.LeftAbsenceDays < absenceDays)
        {
            throw new ArgumentException($"Ученикът няма достатъчно оставащи дни! Оставащи дни: {leftDays}");
        }

        await _absenceRepository.CreateAbsenceAsync(absence);

        absence.SequenceNumber = GenerateSequenceNumber(absence);
        await _absenceRepository.UpdateAbsenceAsync(absence);

        return _mapper.Map<AbsenceDTO>(absence);
    }

    public async Task<AbsenceDTO> UpdateAbsenceAsync(int id, AbsenceDTO absenceDto)
    {
        var existingAbsence = await _absenceRepository.GetAbsenceByIdAsync(id);
        int absenceDays = absenceDto.Days;
        int leftDays = existingAbsence.Student.LeftAbsenceDays;

        if (existingAbsence == null)
        {
            return null;
        }

        if (absenceDto.Start > absenceDto.End || absenceDto.End.ToDateTime(TimeOnly.MinValue) < DateTime.Today)
        {
            throw new ArgumentException("Не може отсъствието да бъде въведено преди днешна дата или началната дата да е по-голяма от крайната!");
        }

        if (leftDays <= 0 || leftDays < absenceDays)
        {
            throw new ArgumentException($"Ученикът няма достатъчно оставащи дни! Оставащи дни: {leftDays}");
        }

        _mapper.Map(absenceDto, existingAbsence);
        await _absenceRepository.UpdateAbsenceAsync(existingAbsence);

        existingAbsence.SequenceNumber = GenerateSequenceNumber(existingAbsence);
        await _absenceRepository.UpdateAbsenceAsync(existingAbsence);

        return _mapper.Map<AbsenceDTO>(existingAbsence);
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
            throw new Exception("Този клас е невалиден");
        }

        var classCode = absence.ClassId;
        var studentId = absence.StudentId.ToString("D2");
        var leftAbsenceDays = absence.Student.LeftAbsenceDays.ToString("D2");
        var currentDate = DateTime.Now.ToString("dd.MM.yy");

        return $"{classCode}-{studentId}-{leftAbsenceDays}/{currentDate}";
    }
}

﻿using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;
using SchoolAdministrationSystem.Utils;

public class AbsenceService : IAbsenceService
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IHolidayRepository _holidayRepository;
    private readonly IMapper _mapper;

    public AbsenceService(IAbsenceRepository absenceRepository, IStudentRepository studentRepository, IClassRepository classRepository, IHolidayRepository holidayRepository, IMapper mapper)
    {
        _absenceRepository = absenceRepository;
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _holidayRepository = holidayRepository; 
        _mapper = mapper;
    }

    public async Task<IEnumerable<AbsenceDTO>> GetAllAbsencesAsync()
    {
        var absences = await _absenceRepository.GetAllAbsencesAsync();
        return _mapper.Map<IEnumerable<AbsenceDTO>>(absences);
    }

    public async Task<PaginatedListUtil<AbsenceDTO>> GetPagedAbsencesAsync(int pageNumber, int pageSize)
    {
        var paginatedAbsences = await _absenceRepository.GetPagedAbsencesAsync(pageNumber, pageSize);
        var count = (await _absenceRepository.GetAllAbsencesAsync()).Count;
        var absenceDTOs = paginatedAbsences.Select(a => _mapper.Map<AbsenceDTO>(a)).ToList();


        return new PaginatedListUtil<AbsenceDTO>(absenceDTOs, count, paginatedAbsences.PageIndex, paginatedAbsences.PageSize);
    }

    public async Task<AbsenceDTO> GetAbsenceByIdAsync(int id)
    {
        var absence = await _absenceRepository.GetAbsenceByIdAsync(id);
        return absence == null ? null : _mapper.Map<AbsenceDTO>(absence);
    }
    public async Task<List<AbsenceDTO>> GetAbsencesByStudentIdAsync(int studentId)
    {
        var absences = await _absenceRepository.GetAllAbsencesByStudentIdAsync(studentId);
        return _mapper.Map<List<AbsenceDTO>>(absences);
    }

    public async Task<List<AbsenceDTO>> GetAbsencesByClassIdAsync(int classId)
    {
        var absences = await _absenceRepository.GetAllAbsencesByClassIdAsync(classId);
        return _mapper.Map<List<AbsenceDTO>>(absences);
    }
    public async Task<List<AbsenceDTO>> GetAbsencesByClassIdPeriodAsync(int classId, DateTime start, DateTime end)
    {
        var absences = await _absenceRepository.GetAllAbsencesByClassIdPeriodAsync(classId, start, end);
        return _mapper.Map<List<AbsenceDTO>>(absences);
    }

    public async Task<AbsenceDTO> CreateAbsenceAsync(AbsenceDTO absenceDto)
    {
        var absence = _mapper.Map<Absence>(absenceDto);

        var allAbsences = await _absenceRepository.GetAllAbsencesAsync();
        var studentAbsences = allAbsences.Where(a => a.StudentId == absence.StudentId);
        var lastAbsence = studentAbsences.OrderByDescending(a => a.End).FirstOrDefault();
        absence.Student = await _studentRepository.GetStudentByIdAsync(absence.StudentId);
        absence.Class = await _classRepository.GetClassByIdAsync(absence.ClassId);
        absence.Days = DaysDifferenceUtil.CalculateWorkingDays(DateOnly.FromDateTime(absence.Start).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(absence.End).ToDateTime(TimeOnly.MinValue), await _holidayRepository.GetHolidaysAsync());

        if (lastAbsence != null)
        {
            var lastAbsenceDays = DaysDifferenceUtil.CalculateWorkingDays(
                DateOnly.FromDateTime(lastAbsence.Start).ToDateTime(TimeOnly.MinValue),
                DateOnly.FromDateTime(lastAbsence.End).ToDateTime(TimeOnly.MinValue),
                await _holidayRepository.GetHolidaysAsync()
            );

            var gapDays = DaysDifferenceUtil.CalculateWorkingDays(
                DateOnly.FromDateTime(lastAbsence.End.AddDays(1)).ToDateTime(TimeOnly.MinValue),
                DateOnly.FromDateTime(absence.Start.AddDays(-1)).ToDateTime(TimeOnly.MinValue),
                await _holidayRepository.GetHolidaysAsync()
            );

            if (gapDays == 0)
            {
                var combinedDays = lastAbsenceDays + absence.Days;
                if (combinedDays > 5)
                {
                    throw new ArgumentException("Ученик не може да използва повече от 5 учебни дни наведнъж, дори и в отделни бележки.");
                }
            }
        }


        if (absence.Student == null || absence.Class == null)
        {
            throw new Exception("Невалиден ученик или клас!");
        }

        if (absence.Start > absence.End)
        {
            throw new ArgumentException("Не може началната дата да е по-голяма от крайната!");
        }

        if (absence.Days > 5)
        {
            throw new ArgumentException("Ученикът не може да използва повече от 5 учебни дни наведнъж!");
        }

        if (absence.Start < DateTime.Today)
        {
            throw new ArgumentException("Не може отсъствието да бъде въведено преди днешна дата!");
        }

        int absenceDays = absence.Days;
        string leftDays = absence.Student.LeftAbsenceDays.ToString();

        if (absence.Student.LeftAbsenceDays <= 0 || absence.Student.LeftAbsenceDays < absenceDays)
        {
            throw new ArgumentException($"Ученикът няма достатъчно оставащи дни! Оставащи дни: {leftDays}");
        }

        foreach (var existingAbsence in studentAbsences)
        {
            if (absence.Start <= existingAbsence.End && absence.End >= existingAbsence.Start)
            {
                throw new ArgumentException("Датите на отсъствието се припокриват с друго отсъствие!");
            }
        }

        await _absenceRepository.CreateAbsenceAsync(absence);

        absence.SequenceNumber = GenerateSequenceNumber(absence);
        await _absenceRepository.UpdateAbsenceAsync(absence);

        return _mapper.Map<AbsenceDTO>(absence);
    }

    public async Task<AbsenceDTO> UpdateAbsenceAsync(int id, AbsenceDTO absenceDto)
    {
        var existingAbsence = await _absenceRepository.GetAbsenceByIdAsync(id);
        absenceDto.Days = DaysDifferenceUtil.CalculateWorkingDays(DateOnly.FromDateTime(absenceDto.Start).ToDateTime(TimeOnly.MinValue), DateOnly.FromDateTime(absenceDto.End).ToDateTime(TimeOnly.MinValue), await _holidayRepository.GetHolidaysAsync());
        int absenceDays = absenceDto.Days;
        int leftDays = existingAbsence.Student.LeftAbsenceDays;

        var allAbsences = await _absenceRepository.GetAllAbsencesAsync();
        var studentAbsences = allAbsences.Where(a => a.StudentId == absenceDto.StudentId);

        if (existingAbsence == null)
        {
            return null;
        }

        if (absenceDto.Days > 5)
        {
            throw new ArgumentException("Ученикът не може да използва повече от 5 учебни дни наведнъж!");
        }
        
        if (absenceDto.Start > absenceDto.End)
        {
            throw new ArgumentException("Не може началната дата да е по-голяма от крайната!");
        }

        if (absenceDto.Start < DateTime.Today)
        {
            throw new ArgumentException("Не може отсъствието да бъде въведено преди днешна дата!");
        }

        if (leftDays <= 0 || leftDays < absenceDays)
        {
            throw new ArgumentException($"Ученикът няма достатъчно оставащи дни! Оставащи дни: {leftDays}");
        }

        foreach (var aliveAbsence in studentAbsences)
        {
            if (absenceDto.Start <= aliveAbsence.End && absenceDto.End >= aliveAbsence.Start)
            {
                throw new ArgumentException("Датите на отсъствието се припокриват с друго отсъствие!");
            }
        }

        existingAbsence.Start = absenceDto.Start;
        existingAbsence.End = absenceDto.End;
        existingAbsence.Reason = absenceDto.Reason;
        existingAbsence.StudentId = absenceDto.StudentId;
        existingAbsence.ClassId = absenceDto.ClassId;

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

    public async Task<PaginatedListUtil<AbsenceDTO>> GetPagedAbsencesByClassIdAsync(int classId, int pageNumber, int pageSize)
    {
        var paginatedAbsences = await _absenceRepository.GetPagedAbsencesByClassIdAsync(classId, pageNumber, pageSize);

        var absenceDTOs = paginatedAbsences.Select(a => _mapper.Map<AbsenceDTO>(a)).ToList();

        return new PaginatedListUtil<AbsenceDTO>(absenceDTOs, paginatedAbsences.TotalPages, paginatedAbsences.PageIndex, paginatedAbsences.PageSize);
    }
}

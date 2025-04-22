using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Include(s => s.Class)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Class)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student.Id;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existingStudent = _context.Students
                .Include(s => s.Absences)
                .FirstOrDefault(s => s.Id == student.Id);

            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.MiddleName = student.MiddleName;
                existingStudent.LastName = student.LastName;
                existingStudent.ClassId = student.ClassId;
                existingStudent.Gender = student.Gender;
                existingStudent.Age = student.Age;
                existingStudent.Address = student.Address;
                existingStudent.PhoneNumber = student.PhoneNumber;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetStudentsByClassIdAsync(int classId)
        {
            return await _context.Students
                .Where(s => s.ClassId == classId)
                .Include(s => s.Class)
                .ToListAsync();
        }

        public async Task CreateStudentsFromRangeAsync(List<Student> students)
        {
            await _context.Students.AddRangeAsync(students);
            await _context.SaveChangesAsync();
        }
    }
}


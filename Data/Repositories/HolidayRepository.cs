using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;

        public HolidayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> GetHolidaysAsync()
        {
            return await _context.Holidays.Select(h => h.Date).ToListAsync();
        }
    }
}

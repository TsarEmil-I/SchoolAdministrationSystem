using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Utils
{
    public static class DaysDifferenceUtil
    {

        public static int CalculateWorkingDays(DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            var h = holidays
                           .Where(h => h.Date >= startDate && h.Date <= endDate)
                           .Select(h => h.Date)
                           .ToList();

            int workingDays = 0;

            DateTime current = startDate;

            while (current <= endDate)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(current))
                {
                    workingDays++;
                }
                current = current.AddDays(1);
            }

            return workingDays;
        }
    }
}


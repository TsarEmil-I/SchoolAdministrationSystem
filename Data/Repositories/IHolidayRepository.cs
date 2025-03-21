namespace SchoolAdministrationSystem.Data.Repositories
{
    public interface IHolidayRepository 
    {
        Task<List<DateTime>> GetHolidaysAsync();
    }
}

using MomoAH.Models;

namespace MomoAH.Interfaces
{
    public interface IBusinessHoursRepository
    {
        Task<IEnumerable<BusinessHours>> GetAllAsync();
        Task<BusinessHours?> GetByDayAsync(string dayOfWeek);
        Task UpdateAsync(BusinessHours businessHours);
    }
}

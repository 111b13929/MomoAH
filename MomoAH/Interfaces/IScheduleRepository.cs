using MomoAH.Models;

namespace MomoAH.Interfaces
{
    /// <summary>
    /// 排班存取的抽象介面。
    /// </summary>
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(string scheduleId);
        Task AddAsync(Schedule schedule);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(string scheduleId);
    }
}

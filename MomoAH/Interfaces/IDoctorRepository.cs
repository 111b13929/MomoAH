using MomoAH.Models;

namespace MomoAH.Interfaces
{
    /// <summary>
    /// 醫師存取的抽象介面。
    /// </summary>
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(string doctorId);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(string doctorId);
    }
}

using MomoAH.Models;

namespace MomoAH.Interfaces
{
    /// <summary>
    /// 科別存取的抽象介面。
    /// </summary>
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty?> GetByIdAsync(string specialtyId);
        Task AddAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
        Task DeleteAsync(string specialtyId);
    }
}

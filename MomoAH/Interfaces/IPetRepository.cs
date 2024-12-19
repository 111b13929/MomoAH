using MomoAH.Models;

namespace MomoAH.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<dynamic> GetByIdAsync(string id);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(string id);
        Task<IEnumerable<Pet>> GetByOwnerIdAsync(string ownerId);

    }
}

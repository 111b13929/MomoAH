using MomoAH.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomoAH.Interfaces
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync(string keyword = "");
        Task<Owner> GetOwnerByIdAsync(string id);
        Task AddOwnerAsync(Owner owner);
        Task UpdateOwnerAsync(Owner owner);
        Task DeleteOwnerAsync(string id);
    }
}

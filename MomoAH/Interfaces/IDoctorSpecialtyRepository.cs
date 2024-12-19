using MomoAH.Models;

namespace MomoAH.Interfaces
{
    public interface IDoctorSpecialtyRepository
    {
        Task<IEnumerable<DoctorSpecialty>> GetAllAsync();
        Task AddAsync(DoctorSpecialty doctorSpecialty);
        Task DeleteAsync(string doctorId, string specialtyId);
    }
}

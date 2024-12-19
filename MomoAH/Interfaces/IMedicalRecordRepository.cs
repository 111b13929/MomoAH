using MomoAH.Models;

namespace MomoAH.Interfaces
{
    /// <summary>
    /// 就診紀錄存取的抽象介面。
    /// </summary>
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord?> GetByIdAsync(string recordId);
        Task AddAsync(MedicalRecord medicalRecord);
        Task UpdateAsync(MedicalRecord medicalRecord);
        Task DeleteAsync(string recordId);
    }
}

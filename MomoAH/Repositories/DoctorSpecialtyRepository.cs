using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class DoctorSpecialtyRepository : IDoctorSpecialtyRepository
    {
        private readonly DbContext _dbContext;

        public DoctorSpecialtyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DoctorSpecialty>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<DoctorSpecialty>(
                "SELECT doctor_id AS DoctorId, specialty_id AS SpecialtyId " +
                "FROM dbo.DoctorSpecialty");
        }


        public async Task AddAsync(DoctorSpecialty doctorSpecialty)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO dbo.DoctorSpecialty (doctor_id, specialty_id) VALUES (@DoctorId, @SpecialtyId)",
                doctorSpecialty);
        }

        public async Task DeleteAsync(string doctorId, string specialtyId)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "DELETE FROM dbo.DoctorSpecialty WHERE doctor_id = @DoctorId AND specialty_id = @SpecialtyId",
                new { DoctorId = doctorId, SpecialtyId = specialtyId });
        }
    }
}

using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly DbContext _dbContext;

        public SpecialtyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Specialty>(
                "SELECT specialty_id AS SpecialtyId, specialty_name AS SpecialtyName " +
                "FROM dbo.Specialty");
        }


        public async Task<Specialty?> GetByIdAsync(string specialtyId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Specialty>(
                "SELECT specialty_id AS SpecialtyId, specialty_name AS SpecialtyName " +
                "FROM dbo.Specialty WHERE specialty_id = @SpecialtyId", new { SpecialtyId = specialtyId });
        }


        public async Task AddAsync(Specialty specialty)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO Specialty (SpecialtyId, SpecialtyName) VALUES (@SpecialtyId, @SpecialtyName)",
                specialty);
        }

        public async Task UpdateAsync(Specialty specialty)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE Specialty SET SpecialtyName = @SpecialtyName WHERE SpecialtyId = @SpecialtyId",
                specialty);
        }

        public async Task DeleteAsync(string specialtyId)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Specialty WHERE SpecialtyId = @SpecialtyId", new { SpecialtyId = specialtyId });
        }
    }
}

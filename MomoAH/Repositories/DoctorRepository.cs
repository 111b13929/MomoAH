using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DbContext _dbContext;

        public DoctorRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Doctor>(
                "SELECT doctor_id AS DoctorId, name AS Name, gender AS Gender, " +
                "phone AS Phone, hire_date AS HireDate " +
                "FROM dbo.Doctor");
        }


        public async Task<Doctor?> GetByIdAsync(string doctorId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Doctor>(
                "SELECT doctor_id AS DoctorId, name AS Name, gender AS Gender, " +
                "phone AS Phone, hire_date AS HireDate " +
                "FROM dbo.Doctor WHERE doctor_id = @DoctorId", new { DoctorId = doctorId });
        }



        public async Task AddAsync(Doctor doctor)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO Doctor (DoctorId, Name, Gender, Phone, HireDate) VALUES (@DoctorId, @Name, @Gender, @Phone, @HireDate)",
                doctor);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE Doctor SET Name = @Name, Gender = @Gender, Phone = @Phone, HireDate = @HireDate WHERE DoctorId = @DoctorId",
                doctor);
        }

        public async Task DeleteAsync(string doctorId)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Doctor WHERE DoctorId = @DoctorId", new { DoctorId = doctorId });
        }
    }
}

using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly DbContext _dbContext;

        public MedicalRecordRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<MedicalRecord>(
                "SELECT record_id AS RecordId, pet_id AS PetId, specialty_id AS SpecialtyId, " +
                "doctor_id AS DoctorId, pet_name AS PetName, visit_date AS VisitDate, " +
                "visit_notes AS VisitNotes " +
                "FROM dbo.MedicalRecord");
        }


        public async Task<MedicalRecord?> GetByIdAsync(string recordId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<MedicalRecord>(
                "SELECT record_id AS RecordId, pet_id AS PetId, specialty_id AS SpecialtyId, " +
                "doctor_id AS DoctorId, pet_name AS PetName, visit_date AS VisitDate, visit_notes AS VisitNotes " +
                "FROM dbo.MedicalRecord WHERE record_id = @RecordId", new { RecordId = recordId });
        }


        public async Task AddAsync(MedicalRecord medicalRecord)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO MedicalRecord (RecordId, PetId, DoctorId, VisitDate, Notes) VALUES (@RecordId, @PetId, @DoctorId, @VisitDate, @Notes)",
                medicalRecord);
        }

        public async Task UpdateAsync(MedicalRecord medicalRecord)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE MedicalRecord SET PetId = @PetId, DoctorId = @DoctorId, VisitDate = @VisitDate, Notes = @Notes WHERE RecordId = @RecordId",
                medicalRecord);
        }

        public async Task DeleteAsync(string recordId)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM MedicalRecord WHERE RecordId = @RecordId", new { RecordId = recordId });
        }
    }
}

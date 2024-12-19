using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DbContext _dbContext;

        public ScheduleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Schedule>(
                "SELECT schedule_id AS ScheduleId, date AS Date, day_of_week AS DayOfWeek, " +
                "shift_time AS ShiftTime, doctor_id AS DoctorId, doctor_name AS DoctorName, " +
                "specialty_id AS SpecialtyId, specialty_name AS SpecialtyName " +
                "FROM dbo.Schedule");
        }



        public async Task<Schedule?> GetByIdAsync(string scheduleId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Schedule>(
                "SELECT schedule_id AS ScheduleId, date AS Date, day_of_week AS DayOfWeek, " +
                "shift_time AS ShiftTime, doctor_id AS DoctorId, doctor_name AS DoctorName, " +
                "specialty_id AS SpecialtyId, specialty_name AS SpecialtyName " +
                "FROM dbo.Schedule WHERE schedule_id = @ScheduleId", new { ScheduleId = scheduleId });
        }


        public async Task AddAsync(Schedule schedule)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO Schedule (ScheduleId, Date, DoctorId, SpecialtyId) VALUES (@ScheduleId, @Date, @DoctorId, @SpecialtyId)",
                schedule);
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE Schedule SET Date = @Date, DoctorId = @DoctorId, SpecialtyId = @SpecialtyId WHERE ScheduleId = @ScheduleId",
                schedule);
        }

        public async Task DeleteAsync(string scheduleId)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Schedule WHERE ScheduleId = @ScheduleId", new { ScheduleId = scheduleId });
        }
    }
}

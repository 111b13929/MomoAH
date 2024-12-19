using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class BusinessHoursRepository : IBusinessHoursRepository
    {
        private readonly DbContext _dbContext;

        public BusinessHoursRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BusinessHours>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<BusinessHours>(
                "SELECT day_of_week AS DayOfWeek, morning_shift AS MorningShift, " +
                "morning_end AS MorningEnd, afternoon_shift AS AfternoonShift, " +
                "afternoon_end AS AfternoonEnd, evening_shift AS EveningShift, " +
                "evening_end AS EveningEnd FROM dbo.BusinessHours");
        }


        public async Task<BusinessHours?> GetByDayAsync(string dayOfWeek)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<BusinessHours>(
                "SELECT day_of_week AS DayOfWeek, morning_shift AS MorningShift, " +
                "morning_end AS MorningEnd, afternoon_shift AS AfternoonShift, " +
                "afternoon_end AS AfternoonEnd, evening_shift AS EveningShift, " +
                "evening_end AS EveningEnd " +
                "FROM dbo.BusinessHours WHERE day_of_week = @DayOfWeek", new { DayOfWeek = dayOfWeek });
        }


        public async Task UpdateAsync(BusinessHours businessHours)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                @"UPDATE dbo.BusinessHours 
                  SET morning_shift = @MorningShift, morning_end = @MorningEnd,
                      afternoon_shift = @AfternoonShift, afternoon_end = @AfternoonEnd,
                      evening_shift = @EveningShift, evening_end = @EveningEnd
                  WHERE day_of_week = @DayOfWeek",
                businessHours);
        }
    }
}

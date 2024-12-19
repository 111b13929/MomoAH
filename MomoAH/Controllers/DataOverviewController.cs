using Microsoft.AspNetCore.Mvc;
using Dapper;
using MomoAH.DataAccess;
using System.Data;
using System.Dynamic;

namespace MomoAH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataOverviewController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public DataOverviewController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("AllTables")]
        public async Task<IActionResult> GetAllTables()
        {
            using var connection = _dbContext.CreateConnection();

            // 定義所有要返回的資料表名稱
            var tableNames = new[] { "Owner", "Pet", "Doctor", "Specialty", "Schedule", "MedicalRecord", "BusinessHours", "DoctorSpecialty" };

            var result = new List<object>();

            foreach (var tableName in tableNames)
            {
                // 查詢資料表的所有數據
                var rows = await connection.QueryAsync($"SELECT * FROM {tableName}");

                // 檢查 rows 是否有內容，避免 null 錯誤
                var firstRow = rows.FirstOrDefault();
                var columns = firstRow != null
                    ? ((IDictionary<string, object>)firstRow).Keys.ToList()
                    : new List<string>();

                var listRows = rows != null
                    ? rows.Select(row => ((IDictionary<string, object>)row).Values.ToList()).ToList()
                    : new List<List<object>>();

                result.Add(new
                {
                    name = tableName,
                    columns = columns,
                    rows = listRows
                });
            }

            return Ok(result);
        }
    }
}

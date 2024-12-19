using System.Data;
using Microsoft.Data.SqlClient;

namespace MomoAH.DataAccess
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;

        // 加上 required 修飾符，強制初始化 _connectionString
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // 確保 _connectionString 正確初始化
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        /// <summary>
        /// 建立 SQL Server 資料庫連線。
        /// </summary>
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}

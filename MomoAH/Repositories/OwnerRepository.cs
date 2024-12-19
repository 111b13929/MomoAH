using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomoAH.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DbContext _dbContext;

        public OwnerRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync(string keyword = "")
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
                SELECT owner_id AS OwnerId, name AS Name, gender AS Gender, 
                       phone AS Phone, address AS Address
                FROM dbo.Owner
                WHERE @Keyword = '' OR name LIKE '%' + @Keyword + '%' OR phone LIKE '%' + @Keyword + '%'
            ";

            return await connection.QueryAsync<Owner>(query, new { Keyword = keyword });
        }

        public async Task<Owner> GetOwnerByIdAsync(string id)
        {
            using var connection = _dbContext.CreateConnection();
            var query = "SELECT owner_id AS OwnerId, name, gender, phone, address FROM dbo.Owner WHERE owner_id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Owner>(query, new { Id = id });
        }

        public async Task AddOwnerAsync(Owner owner)
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
        INSERT INTO dbo.Owner (owner_id, name, gender, phone, address)
        VALUES (NEWID(), @Name, @Gender, @Phone, @Address)";
            await connection.ExecuteAsync(query, owner);
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
        UPDATE dbo.Owner
        SET name = @Name, gender = @Gender, phone = @Phone, address = @Address
        WHERE owner_id = @OwnerId";
            await connection.ExecuteAsync(query, owner);
        }


        public async Task DeleteOwnerAsync(string id)
        {
            using var connection = _dbContext.CreateConnection();
            var query = "DELETE FROM dbo.Owner WHERE owner_id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}

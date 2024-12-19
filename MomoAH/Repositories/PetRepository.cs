using Dapper;
using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Models;

namespace MomoAH.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DbContext _dbContext;

        public PetRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
        SELECT p.pet_id AS PetId, 
               p.name AS Name, 
               p.gender AS Gender, 
               p.birth_date AS BirthDate, 
               p.breed AS Breed, 
               p.owner_id AS OwnerId, 
               o.name AS OwnerName -- 飼主姓名
        FROM dbo.Pet p
        LEFT JOIN dbo.Owner o ON p.owner_id = o.owner_id";

    return await connection.QueryAsync<Pet>(query);
        }



        public async Task<dynamic> GetByIdAsync(string id)
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
                SELECT p.pet_id AS PetId, p.name, p.gender, p.birth_date AS BirthDate, 
                       p.breed, p.owner_id AS OwnerId, o.name AS OwnerName
                FROM Pet p
                LEFT JOIN Owner o ON p.owner_id = o.owner_id
                WHERE p.pet_id = @PetId";

            var result = await connection.QuerySingleOrDefaultAsync<dynamic>(query, new { PetId = id });
            if (result == null) throw new InvalidOperationException("找不到指定的寵物資料");
            return result;
        }

        public async Task AddAsync(Pet pet)
        {
            // 自動生成唯一 PetId（使用 GUID 截斷部分字串）
            pet.PetId = Guid.NewGuid().ToString("N").Substring(0, 10);

            using var connection = _dbContext.CreateConnection();
            var query = @"
        INSERT INTO Pet (pet_id, name, gender, birth_date, breed, owner_id)
        VALUES (@PetId, @Name, @Gender, @BirthDate, @Breed, @OwnerId)";
            await connection.ExecuteAsync(query, pet);
        }


        public async Task UpdateAsync(Pet pet)
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
                UPDATE Pet
                SET name = @Name, gender = @Gender, birth_date = @BirthDate, 
                    breed = @Breed, owner_id = @OwnerId
                WHERE pet_id = @PetId";
            var rowsAffected = await connection.ExecuteAsync(query, pet);

            if (rowsAffected == 0) throw new InvalidOperationException("更新失敗，找不到指定的寵物資料");
        }

        public async Task DeleteAsync(string id)
        {
            using var connection = _dbContext.CreateConnection();
            var query = "DELETE FROM Pet WHERE pet_id = @PetId";
            var rowsAffected = await connection.ExecuteAsync(query, new { PetId = id });

            if (rowsAffected == 0) throw new InvalidOperationException("刪除失敗，找不到指定的寵物資料");
        }

        public async Task<IEnumerable<Pet>> GetByOwnerIdAsync(string ownerId)
        {
            using var connection = _dbContext.CreateConnection();
            var query = @"
        SELECT pet_id AS PetId, name, gender, birth_date AS BirthDate, breed, owner_id AS OwnerId 
        FROM Pet
        WHERE owner_id = @OwnerId";
            return await connection.QueryAsync<Pet>(query, new { OwnerId = ownerId });
        }

    }
}

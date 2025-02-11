using Dal.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace Dal.SqlServer.Infrastructure
{
    public class SqlCategoryRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), ICategoryRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAsync(Category category)
        {
            var sql = @"Insert INTO Categories([Name],[CreatedBy])
                        VALUES (@Name, @CreatedBy)";
            using var conn = OpenConnection();
            await conn.QueryAsync(sql, category);
        }

        public bool Delete(int id, int deletedBy)
        {
            var checkSql = @"SELECT Id FROM Categories WHERE Id = @id AND IsDeleted=0";
            var sql = @"UPDATE Categories
                        SET IsDeleted=1,
                        DeletedBy = @deletedBy
                        DeletedDate = GETDATE()
                        WHERE Id=@id";
            using var conn = OpenConnection();
            using var transaction = conn.BeginTransaction();

            var categoryId = conn.ExecuteScalar<int?>(checkSql, id, transaction);

            if(!categoryId.HasValue)
                return false;

            var affectRows = conn.Execute(sql, new {id,deletedBy},transaction);
            transaction.Commit();
            return affectRows > 0;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories.OrderByDescending(c=> c.CreatedBy).Where(c=>c.IsDeleted==false);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var sql = @"SELECT C.*
                        FROM Categories AS C
                        WHERE C.Id = @id AND C.IsDeleted = 0";
            using var conn = OpenConnection();
            return await conn.QueryFirstOrDefaultAsync<Category>(sql,id);
        }

        public void Update(Category category)
        {
            var sql = @"UPDATE Categories 
                        SET Name = @Name,
                        UpdateBy = @UpdatedBy,
                        UpdatedDate = GETDATE()
                        WHERE Id = @Id";
            using var conn = OpenConnection();

            conn.Query(sql, category);
        }
    }
}

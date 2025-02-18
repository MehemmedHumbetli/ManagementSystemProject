using Dal.SqlServer.Context;
using Dal.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace Dal.SqlServer.UnitOfWork
{
    public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
    {
        private readonly string _connectionString = connectionString;
        private readonly AppDbContext _context = context;

        public SqlCategoryRepository categoryRepository;
        public SqlUserRepository userRepository;

        public ICategoryRepository CategoryRepository => categoryRepository ?? new SqlCategoryRepository(_connectionString,_context);

        public IUserRepository UserRepository => userRepository ?? new SqlUserRepository(_context);
    }
}

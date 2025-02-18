using Dal.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.SqlServer.Infrastructure
{
    public class SqlUserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public IQueryable<User> GetAll()
        {
            return _appDbContext.Users;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return (await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email))!;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return (await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id)!);
        }

        public async Task RegisterAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
        }

        public async Task Remove(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.IsDeleted = true;
            user.DeletedDate = DateTime.Now;
            user.DeletedBy = 0;
        }

        public void Update(User user)
        {
            user.UpdatedDate = DateTime.Now;
            _appDbContext.Update(user);
        }
    }
}

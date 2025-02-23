﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.SqlServer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

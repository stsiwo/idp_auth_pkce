using IdentityServer.Infrastracture.DataEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Infrastracture
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext()
        {
            
        }

        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasData(
                new { Id = 1, Title = "task 1", IsDone = true, Priority = 1 },
                new { Id = 2, Title = "task 2", IsDone = false, Priority = 2 },
                new { Id = 3, Title = "task 3", IsDone = true, Priority = 3 },
                new { Id = 4, Title = "task 4", IsDone = false, Priority = 4 },
                new { Id = 5, Title = "task 5", IsDone = true, Priority = 5 },
                new { Id = 6, Title = "task 6", IsDone = true, Priority = 6 },
                new { Id = 7, Title = "task 7", IsDone = false, Priority = 7 },
                new { Id = 8, Title = "task 8", IsDone = true, Priority = 8 },
                new { Id = 9, Title = "task 9", IsDone = true, Priority = 9 }
                );
        }

        public DbSet<ToDo> ToDos { get; set; }

    }

}

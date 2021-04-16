using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Model
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}

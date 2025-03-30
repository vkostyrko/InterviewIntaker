using InterviewIntaker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewIntaker.Data.DbContext
{
    /// <summary>
    /// Db context
    /// </summary>
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /// <inheritdoc />
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets task db set
        /// </summary>
        internal DbSet<TaskEntity> Tasks { get; set; }
    }
}

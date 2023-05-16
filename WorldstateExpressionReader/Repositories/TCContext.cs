using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldstateExpressionReader.Repositories
{
    internal class TCContext : DbContext, IDbContext
    {
        private readonly string _connectionString;

        public TCContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            //options.UseInMemoryDatabase("TestDb");
            options.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WorldstateNameTC>(x => x.ToTable("world_state"));
            builder.Entity<WorldstateExpression>(x => x.ToTable("worldstate_expression"));
        }

        public DbSet<WorldstateNameTC> WorldstateName { get; set; }
        public DbSet<WorldstateExpression> WorldstateExpression { get; set; }

        public async Task<string?> GetWorldstateName(int Id)
        {
            var result = await WorldstateName.SingleOrDefaultAsync(p => p.Id == Id);
            return result != null ? result.Comment : null;
        }

        public async Task<string?> GetWorldstateExpression(int Id)
        {
            var result = await WorldstateExpression.SingleOrDefaultAsync(p => p.Id == Id);
            return result != null ? result.Expression : null;
        }

        public async Task<List<WorldstateExpression>?> GetAllExpressions()
        {
            return await WorldstateExpression.ToListAsync();
        }
    }
}

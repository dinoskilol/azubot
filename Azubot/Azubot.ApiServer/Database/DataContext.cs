using Azubot.ApiServer.Configuration;
using Azubot.ApiServer.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Azubot.ApiServer.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly AppConfiguration Configuration;

        public DataContext(AppConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var config = Configuration.Database;

            var connectionString = $"host={config.Host};" +
                                   $"port={config.Port};" +
                                   $"database={config.Database};" +
                                   $"uid={config.Username};" +
                                   $"pwd={config.Password}";

            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                builder =>
                {
                    builder.EnableRetryOnFailure(5);
                    builder.MigrationsHistoryTable("MigrationHistory");
                }
            );
        }
    }
}
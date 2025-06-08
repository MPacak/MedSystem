using Microsoft.EntityFrameworkCore;
using Npgsql;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(DbContextOptionsBuilder options, string connectionString)
    {
        var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Pooling = true,
            MaxPoolSize = 20,
            MinPoolSize = 5,
            ConnectionIdleLifetime = 300
        };

        options.UseNpgsql(npgsqlBuilder.ConnectionString,
            npgsqlOptions => {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
                npgsqlOptions.MigrationsAssembly("DAL");
            });
    }
}
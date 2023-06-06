using DbUp;
using Microsoft.Extensions.Configuration;

namespace Boss.Gateway.Persistence;
public static class DbUpMigrator
{
    public static void MigrateDatabase(IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString:Postgres"];
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        var upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .LogToConsole()
        .WithScriptsFromFileSystem("../../Boss.Gateway/Boss.Gateway.Persistence/Migrations")
        .Build();
        var result = upgrader.PerformUpgrade();
        if (result.Successful)
        {
            Console.WriteLine("Database upgrade complete");
        }
        else
        {
            throw new Exception("Database migration failed.", result.Error);
        }
    }
}
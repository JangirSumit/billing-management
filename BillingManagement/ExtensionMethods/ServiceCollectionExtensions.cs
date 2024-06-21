using BillingManagement.DB.Sqlite;
using BillingManagement.DB.SqlServer;
using SQLitePCL;

namespace BillingManagement.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string? useDb)
    {
        if (useDb == "Sqlite")
        {
            Batteries.Init();
            services.RegisterSqliteServices();
        }
        else
        {
            services.RegisterSqlServerServices();
        }

        return services;
    }
}

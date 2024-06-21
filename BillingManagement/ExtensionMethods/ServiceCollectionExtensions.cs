using BillingManagement.DB.Sqlite;
using BillingManagement.DB.SqlServer;

namespace BillingManagement.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string? useDb)
    {
        if (useDb == "Sqlite")
        {
            services.RegisterSqliteServices();
        }
        else
        {
            services.RegisterSqlServerServices();
        }

        return services;
    }
}

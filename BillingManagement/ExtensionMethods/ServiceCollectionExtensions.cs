using SqliteDataAccess = BillingManagement.DB.Sqlite.DataAccess;
using SqlServerDataAccess = BillingManagement.DB.SqlServer.DataAccess;
using BillingManagement.DB.SqlServer.Repository;
using BillingManagement.Contracts.Abstrations;


namespace BillingManagement.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string? useDb)
    {
        if (useDb == "Sqlite")
        {
            services.AddScoped<IDataAccess, SqliteDataAccess>();
        }
        else
        {
            services.AddScoped<IDataAccess, SqlServerDataAccess>().
                AddScoped<IVendorsRepository, VendorsRepository>().
            AddScoped<IItemsRepository, ItemsRepository>().
            AddScoped<IUsersRepository, UsersRepository>();
        }

        return services;
    }
}

using BillingManagement.Repository.Abstrations;
using BillingManagement.Repository;
using BillingManagement.DB;

using SqliteDataAccess = BillingManagement.DB.Sqlite.DataAccess;
using SqlServerDataAccess = BillingManagement.DB.SqlServer.DataAccess;


namespace BillingManagement.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string? useDb)
    {
        if (useDb == "Sqlite")
            services.AddScoped<IDataAccess, SqliteDataAccess>();
        else
            services.AddScoped<IDataAccess, SqlServerDataAccess>();

        return services.AddScoped<IVendorsRepository, VendorsRepository>().
            AddScoped<IItemsRepository, ItemsRepository>().
            AddScoped<IUsersRepository, UsersRepository>();
    }
}

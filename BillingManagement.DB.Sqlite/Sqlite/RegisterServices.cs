using BillingManagement.Contracts.Abstrations;
using BillingManagement.DB.Sqlite.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BillingManagement.DB.Sqlite;

public static class RegisterServices
{
    public static IServiceCollection RegisterSqliteServices(this IServiceCollection services)
    {
        return services.AddScoped<IDataAccess, DataAccess>().
                     AddScoped<IVendorsRepository, VendorsRepository>().
                     AddScoped<IItemsRepository, ItemsRepository>().
                     AddScoped<IUsersRepository, UsersRepository>();
    }
}

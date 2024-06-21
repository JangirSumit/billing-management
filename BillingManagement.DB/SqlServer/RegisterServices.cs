using BillingManagement.Contracts.Abstrations;
using BillingManagement.DB.SqlServer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BillingManagement.DB.SqlServer;

public static class RegisterServices
{
    public static IServiceCollection RegisterSqlServerServices(this IServiceCollection services)
    {
        return services.AddScoped<IDataAccess, DataAccess>().
                     AddScoped<IVendorsRepository, VendorsRepository>().
                     AddScoped<IItemsRepository, ItemsRepository>().
                     AddScoped<IUsersRepository, UsersRepository>();
    }
}

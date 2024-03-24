using Accounts.Db.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Db;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AccountsContext>(); // added as a Scoped resource
        return services;
    }
}
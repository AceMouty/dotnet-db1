using Accounts.Api.Endpoints.Accounts;

namespace Accounts.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountsEndpoints();
        return app;
    }
}
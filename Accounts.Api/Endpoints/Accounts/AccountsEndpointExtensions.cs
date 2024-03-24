namespace Accounts.Api.Endpoints.Accounts;

public static class AccountsEndpointExtensions
{
    public static IEndpointRouteBuilder MapAccountsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetAllAccounts();
        app.MapGetAccountById();
        app.MapCreateAccount();
        app.MapUpdateAccountById();
        app.MapDeleteAccountById();
        return app;
    }
}
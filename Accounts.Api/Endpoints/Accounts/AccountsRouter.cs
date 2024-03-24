using Accounts.Api.Endpoints.Accounts.Contracts;
using Accounts.Db.Data;
using Accounts.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Endpoints.Accounts;

public static class AccountsRouter
{
    public static IEndpointRouteBuilder MapGetAllAccounts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/accounts", async (AccountsContext dbContext) => TypedResults.Ok(await dbContext.Accounts.ToListAsync()));
        return app;
    }

    public static IEndpointRouteBuilder MapGetAccountById(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/accounts/{id:int}", async (int id, AccountsContext context) =>
        {
            // Similar to first or default, but throws if more than one match is found
            // Account? account = await context.Accounts.SingleOrDefaultAsync(a => a.Id == id);
            
            // EF will cache already queried objects, FindAsync will server from cache if able else queries DB and caches result
            // Account? account = await context.Accounts.FindAsync(id); // queries off PK
            
            // Query database, returns first matching result or null if not found / does not exist.
            Account? account = await context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            return account is null ? Results.NotFound() : TypedResults.Ok(account);
        })
        .WithName("GetAccountById")
        .Produces<Account>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
    
    public static IEndpointRouteBuilder MapCreateAccount(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/accounts", async (CreateAccountRequest newAccount, AccountsContext context) =>
        {
            Account accountEntity = new Account { Name = newAccount.Name, Budget = newAccount.Budget };
            await context.Accounts.AddAsync(accountEntity);
            await context.SaveChangesAsync();

            return TypedResults.CreatedAtRoute(accountEntity, "GetAccountById", new { id = accountEntity.Id });
        });

        return app;
    }
    
    public static IEndpointRouteBuilder MapUpdateAccountById(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/accounts/{id:int}", async (int id, AccountsContext context, UpdateAccountRequest request) =>
            {
                Account? foundAccount = await context.Accounts.FindAsync(id);
                if (foundAccount is null)
                {
                    return Results.NotFound();
                }

                foundAccount.Name = request.Name;
                foundAccount.Budget = request.Budget;

                await context.SaveChangesAsync();
            
            
                return Results.Ok(foundAccount);
            })
        .Produces<Account>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }

    public static IEndpointRouteBuilder MapDeleteAccountById(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/accounts/{id}", async (int id, AccountsContext context) =>
        {
            Account? foundAccount = await context.Accounts.FindAsync(id);
            if (foundAccount is null)
            {
                return Results.NotFound();
            }

            context.Accounts.Remove(foundAccount);
            await context.SaveChangesAsync();

            return Results.Ok(foundAccount);
        });

        return app;
    }
    
}
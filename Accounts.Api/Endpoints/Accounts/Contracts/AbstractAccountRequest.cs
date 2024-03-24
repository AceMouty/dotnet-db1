namespace Accounts.Api.Endpoints.Accounts.Contracts;

public abstract class AbstractAccountRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
}
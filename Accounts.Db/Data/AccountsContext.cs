using Accounts.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Db.Data;

public class AccountsContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>(); // produces non nullable DB Set
    
    // Register a DB
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: externalize the connection string
        optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=AccountsDB;User Id=sa;Password=SaPassword1234!;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
}
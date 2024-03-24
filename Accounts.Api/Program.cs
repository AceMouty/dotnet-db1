using Accounts.Api.Endpoints;
using Accounts.Db;
using Accounts.Db.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Accounts.Db registration
builder.Services.AddDatabase();

WebApplication app = builder.Build();

// TODO: HACK, fix this at some point...for now just need things working
IServiceScope scope = app.Services.CreateScope();
AccountsContext context = scope.ServiceProvider.GetRequiredService<AccountsContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map route handlers for the API application
app.MapApiEndpoints();

app.Run();
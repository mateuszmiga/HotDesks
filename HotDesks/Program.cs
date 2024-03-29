using Data.EFCore.DbContext;
using HotDesks.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddServices(builder);
builder.Services.AddIdentityServices();
builder.Services.ConfigureJWT(configuration);
builder.Host.UseSerilog((hbc, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("logs.txt")
    .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

using var scope = app.Services.CreateScope();
var dataSeeder = new DataSeeder();
var context = scope.ServiceProvider.GetService<Context>();

await dataSeeder.SeedDataAsyncIfDbIsEmpty(context);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
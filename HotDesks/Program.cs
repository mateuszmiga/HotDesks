using System.Text.Json.Serialization;
using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Entities;
using Domain.Interfaces;
using HotDesks.Api.Mappings;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Context>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(DeskProfile));
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

app.UseAuthorization();

app.MapControllers();

app.Run();

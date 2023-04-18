using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Interfaces;
using HotDesks.Api.Mappings;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HotDesks.Api.Extensions
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection seviceCollection, WebApplicationBuilder builder)
        {
            seviceCollection.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            seviceCollection.AddEndpointsApiExplorer();
            seviceCollection.AddSwaggerGen();
            seviceCollection.AddDbContext<Context>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            seviceCollection.AddScoped<Context>();
            seviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            seviceCollection.AddAutoMapper(typeof(DeskProfile));
        }
    }
}

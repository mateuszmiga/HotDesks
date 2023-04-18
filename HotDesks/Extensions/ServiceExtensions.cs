using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Interfaces;
using HotDesks.Api.Mappings;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotDesks.Api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds all services for application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<Context>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<Context>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(DeskProfile));    
            services.AddAuthentication();
        }

        /// <summary>
        /// Adds ASP.NET Identity service with specific configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(u => u.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<Context>();
        }
    }
}

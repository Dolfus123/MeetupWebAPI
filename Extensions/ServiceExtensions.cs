using MeetupWebAPI.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureNpgsqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["NpgsqlConnection:ConnectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseNpgsql(connectionString,
                assembly => assembly.MigrationsAssembly("MeetupAPI")));
        }
    }
}

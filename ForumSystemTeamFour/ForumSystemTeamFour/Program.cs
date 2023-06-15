using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ForumSystemTeamFour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // Data persistence
            builder.Services.AddDbContext<ForumDbContext>(options =>
            {
                var connectionString = builder.Configuration["ForumSystem:DevConnectionString"];
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumSystemAPI", Version = "v1" });
            });

            // Repositories
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ITagsRepository, TagsRepository>();
            
            // Services
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITagServices, TagServices>();
            
            // Helpers
            builder.Services.AddScoped<UserMapper>();
            builder.Services.AddScoped<SecurityServices>();

            var app = builder.Build();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreDemo API V1");
                options.RoutePrefix = "api/swagger";
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
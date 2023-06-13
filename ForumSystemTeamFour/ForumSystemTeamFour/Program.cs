using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            // Repositories
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ITagsRepository, TagsRepository>();
            
            // Services
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITagServices, TagServices>();
            
            // Helpers
            builder.Services.AddScoped<UserMapper>();
            
            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
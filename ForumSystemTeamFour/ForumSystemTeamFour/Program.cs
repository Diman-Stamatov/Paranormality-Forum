using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
            builder.Services.AddSingleton<ITagsRepository, TagsRepository>();
            

            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITagServices, TagServices>();
            

            builder.Services.AddScoped<UserMapper>();
            
            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
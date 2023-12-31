using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForumSystemTeamFour
{
    
    public class Program
    {
        
        public static void Main(string[] args)
        {   
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Data persistence
            builder.Services.AddDbContext<ForumDbContext>(options =>
            {
                var connectionString = builder.Configuration["ForumSystem:DevConnectionString"];
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumSystemAPI", Version = "v1" });                
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Repositories
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ITagsRepository, TagsRepository>();
            builder.Services.AddScoped<IRepliesRepository, RepliesRepository>();
            builder.Services.AddScoped<IThreadRepositroy, ThreadRepository>();
            
            // Services
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ISecurityServices, SecurityServices>();
            builder.Services.AddScoped<ITagServices, TagServices>();
            builder.Services.AddScoped<IReplyService, ReplyService>();
            builder.Services.AddScoped<IThreadService, ThreadService>();

            // Helpers
            builder.Services.AddScoped<ITagMapper, TagMapper>();
            builder.Services.AddScoped<IUserMapper, UserMapper>();
            
            builder.Services.AddScoped<IReplyMapper, ReplyMapper>();
            builder.Services.AddScoped<IThreadMapper, ThreadMapper>();
            builder.Services.AddScoped<IThreadVoteMapper, ThreadVoteMapper>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {  
                    OnMessageReceived = context => {
                        context.Token = context.Request.Cookies["Cookie_JWT"];
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddMvc();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseRouting();            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreDemo API V1");
                options.RoutePrefix = "api/swagger";
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
             
            });

            app.Run();
        }
    }
}
using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
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

namespace ForumSystemTeamFour
{
    
    public class Program
    {
        public static WebApplicationBuilder Builder = null;
        public static void Main(string[] args)
        {
            Builder = WebApplication.CreateBuilder(args);
            Builder.Services.AddControllers();

            // Data persistence
            Builder.Services.AddDbContext<ForumDbContext>(options =>
            {
                var connectionString = Builder.Configuration["ForumSystem:DevConnectionString"];
                options.UseSqlServer(connectionString);
            });

            Builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumSystemAPI", Version = "v1" });
            });

            // Repositories
            Builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            Builder.Services.AddScoped<ITagsRepository, TagsRepository>();
            
            // Services
            Builder.Services.AddScoped<IUserServices, UserServices>();
            Builder.Services.AddScoped<ISecurityServices, SecurityServices>();
            Builder.Services.AddScoped<ITagServices, TagServices>();
            
            // Helpers
            Builder.Services.AddScoped<UserMapper>();
            Builder.Services.AddScoped<SecurityServices>();

            Builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Builder.Configuration["Jwt:Issuer"],
                    ValidAudience = Builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            Builder.Services.AddAuthorization();

            var app = Builder.Build();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreDemo API V1");
                options.RoutePrefix = "api/swagger";
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers().RequireAuthorization();
             
            });

            app.Run();
        }
    }
}
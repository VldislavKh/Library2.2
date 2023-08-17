using Domain.Infrastructure;
using Library2._2.Interfaces.AuthInterfaces;
using Library2._2.Interfaces.AuthorInterfaces;
using Library2._2.Interfaces.BookInterfaces;
using Library2._2.Interfaces.RoleInterfaces;
using Library2._2.Interfaces.UserInterfaces;
using Library2._2.Options;
using Library2._2.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;
using Library2._2.CustomExceptionMiddleware;
using Serilog.Debugging;
using Library2._2.Logging;

namespace Library2._2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            builder.Host.UseSerilog((hostBuilder, loggerConfiguration) =>
            {
                var envName = builder.Environment.EnvironmentName.ToLower().Replace(".", "-");
                var yourAppName = "your-app-name";
                var yourTemplateName = "your-template-name";

                loggerConfiguration
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                    {
                        IndexFormat = $"{yourAppName}-{envName}-{DateTimeOffset.Now:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        OverwriteTemplate = true,
                        TemplateName = yourTemplateName,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                        TypeName = null,
                        BatchAction = ElasticOpType.Create,
                    });
            });

            //Вывод ошибок в консоль.
            SelfLog.Enable(Console.Error);

            var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddHangfire(x =>
            //     x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddHangfireServer();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "127.0.0.1:6379";
            //    //options.InstanceName = "local";
            //});

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            builder.Services.AddScoped<IAddDeleteAuthor, AuthorService>();
            builder.Services.AddScoped<IGetAuthorsInfo, AuthorService>();
            builder.Services.AddScoped<IAddDeleteBook, BookService>();
            builder.Services.AddScoped<IGetBooksInfo, BookService>();
            builder.Services.AddScoped<IAddDeleteUser, UserService>();
            builder.Services.AddScoped<IGetUsersInfo, UserService>();
            builder.Services.AddScoped<IAddDeleteRole, RoleService>();
            builder.Services.AddScoped<IGetRolesInfo, RoleService>();
            builder.Services.AddScoped<ISetRole, RoleService>();
            builder.Services.AddScoped<IAuth, AuthService>();
            builder.Services.AddScoped<IGenerateJwt, AuthService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            //app.UseHangfireDashboard("/dashboard"); //Will be available under http://localhost:5000/hangfire"
            //app.UseHangfireServer();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.ConfigureCustomExceptionMiddleware();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
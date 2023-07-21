using Library2._2.Infrastructure;
using Library2._2.Interfaces.AuthorInterfaces;
using Library2._2.Interfaces.BookInterfaces;
using Library2._2.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Library2._2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IAddDeleteAuthor, AuthorService>();
            builder.Services.AddScoped<IGetAuthorsInfo, AuthorService>();
            builder.Services.AddScoped<IAddDeleteBook, BookService>();
            builder.Services.AddScoped<IGetBooksInfo, BookService>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
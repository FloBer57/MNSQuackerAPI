using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using QuackerMNS.infrastructure.Database;

namespace QuackerMNS
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Ajoutez les services à l'application
            builder.Services.AddControllers();
            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configurez Entity Framework Core pour utiliser MySQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // L'ordre est important: Authentication avant Authorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

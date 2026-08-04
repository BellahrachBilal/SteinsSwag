using Microsoft.EntityFrameworkCore;
using SteinsSwag.Application.Interfaces;
using SteinsSwag.Application.Services;
using SteinsSwag.Infrastructure.Data;
using Scalar.AspNetCore;
namespace SteinsSwag.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<SteinsSwagDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //services
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<ISellerService, SellerService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            //controllers + JSON enum-as-string
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularDevCors", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();

            }

            app.UseHttpsRedirection();
            app.UseCors("AngularDevCors");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

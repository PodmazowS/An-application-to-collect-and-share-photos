using Domain.Services;
using ApplicationLayer.AppServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Domain.Models;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using MongoDB.Bson;
using AspNetCore.Identity.MongoDbCore;
using Microsoft.AspNetCore.Identity;


namespace An_application_to_collect_and_share_photos
{
    public class Program
    {
        private readonly IConfiguration _configuration;

        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var program = new Program(configuration);
            program.Run();
        }

        public void Run()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            // Configure MongoDB
            builder.Services.Configure<MongoDBSettings>(_configuration.GetSection("MongoDB"));
            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionUri);
            });
            builder.Services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return client.GetDatabase(settings.DatabaseName);
            });
            builder.Services.AddScoped<MongoDBContext<User, UserRole, ObjectId>>();

            // Configure Identity with MongoDB stores
            builder.Services.AddIdentity<User, UserRole>()
                .AddMongoDbStores<User, UserRole, ObjectId>("mongodb+srv://ivanadmin:admin123%24@photosharingapplication.b6syjhf.mongodb.net/?retryWrites=true&w=majority",
                "photosharingappdb") // Додано налаштування Identity з використанням MongoDB
                .AddDefaultTokenProviders();

            // Configure Services
            builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();

            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "account",
                pattern: "Account/SignUpAsync",
                defaults: new { controller = "Account", action = "SignUpAsync" });

            app.Run();
        }
    }
}

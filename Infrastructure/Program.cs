using Domain.Models;
using Infrastructure.Data;
using Infrastructure.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
            builder.Services.AddSingleton<MongoDBContext>();
 
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


 /*           var dbContext = app.Services.GetRequiredService<MongoDBContext>();

            var photo = new Photo
            {
                Url = "https://example.com/photo.jpg",
                Title = "New Photo",
                Description = "This is a new photo",
                AlbumId = ObjectId.GenerateNewId(),
                UserId = ObjectId.GenerateNewId(),
                CameraName = "Canon",
                Status = "Active",
                UploadDate = DateTime.Now,
                Tag = "Nature"
            };

            dbContext.Photos.InsertOne(photo);

            Console.WriteLine("Photo added to collection.");
 */

            app.Run();
        }
    }
}
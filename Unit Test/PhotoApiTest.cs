using Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.MongoDB;
using ApplicationLayer.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Unit_Test
{
    public class PhotoApiTest : IClassFixture<WebApplicationFactory<Program>>
    {

 

        //Arrange for all methods 
        private readonly HttpClient _client;



        public PhotoApiTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        //FindAll
        [Fact]//it's correct        
        public async void GetShouldReturnCountPhotos()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetFromJsonAsync<List<PhotoDto>>("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(6, result.Count);//Equal(9, -   count photos in Db
        }



        //OkStatusForPhoto
        [Fact]//it's correct
        public async void GetShouldReturnOkStatusForPhoto()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetAsync("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //FindById

        //FindById - X
        [Fact]
        public async Task Get_Object_Returns_SuccessForFindById()
        {
            // Arrange
            //int objectId = 1; // Замініть на ідентифікатор існуючого об'єкта

            // Arrange
            var id = ObjectId.GenerateNewId();

            var photo = new Photo//Kiedy wchodzimy Object Photo wyskakuje błędy
            {
                Id = id,//!
                Url = "https://example.com/photo.jpg",
                Title = "Sample Photo",
                Description = "This is a sample photo",
                CameraName = "Nikon",
                Status = "Active",
                UploadDate = DateTime.Now,
                Tag = "Sample",
                UserId = ObjectId.GenerateNewId()//!
            };

            // Act
            var result = await _client.GetAsync($"/api/PhotoControllerMongoDb/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var photoDto = Assert.IsType<PhotoDto>(okResult.Value);

            Assert.Equal(photo.Id.ToString(), photoDto.Id);
            Assert.Equal(photo.Url, photoDto.Url);
            Assert.Equal(photo.Title, photoDto.Title);
            Assert.Equal(photo.Description, photoDto.Description);
            Assert.Equal(photo.CameraName, photoDto.CameraName);
            Assert.Equal(photo.Status, photoDto.Status);
            Assert.Equal(photo.UploadDate, photoDto.UploadDate);
            Assert.Equal(photo.Tag, photoDto.Tag);
            Assert.Equal(photo.UserId.ToString(), photoDto.UserId);
            //Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //GetPhotosByUserId


        //GetPhotosByUserId_ReturnsCorrectPhotos - X
        [Fact]
        public async Task GetPhotosByUserId_ReturnsCorrectPhotos()
        {
            // Arrange
            var userId = new ObjectId("6151dc195dc68dab2c80c66e");
            var photos = new List<Photo>
            {
                new Photo
                {
                    Id = new ObjectId("6151dc195dc68dab2c80c66d"),
                    Url = "https://example.com/photo1.jpg",
                    Title = "Photo 1",
                    Description = "This is photo 1",
                    CameraName = "Canon EOS 5D Mark IV",
                    Status = "Active",
                    UploadDate = DateTime.Now,
                    Tag = "Nature",
                    UserId = userId
                }
            };
            //_photoServiceMock.Setup(x => x.GetPhotosByUserIdAsync(userId)).ReturnsAsync(photos);


            // Act
            //var result = await _photoController.FindByUserId(userId) as OkObjectResult;

            var result = await _client.GetFromJsonAsync<List<PhotoDto>>($"/api/PhotoControllerMongoDb/{userId}");
            var actualPhotos = result as IEnumerable<Photo>;

            // Assert
            Assert.Equal(1, actualPhotos.Count());
            Assert.Contains(actualPhotos, p => p.Title == "Photo 1");

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //UpdatePhoto

        /*
        [Fact]
        public async Task UpdatePhoto_ReturnsOkResult()
        {
            // Arrange
            var photoId = "6151dc195dc68dab2c80c66d";
            var photoDto = new PhotoDto
            {
                Url = "https://example.com/photo1.jpg",
                Title = "Updated Photo",
                Description = "This is the updated photo",
                CameraName = "Canon EOS 5D Mark IV",
                Status = "Active",
                UploadDate = DateTime.Now,
                Tag = "Nature"
            };
            var updatedPhoto = new Photo
            {
                Id = new ObjectId(photoId),
                Url = photoDto.Url,
                Title = photoDto.Title,
                Description = photoDto.Description,
                CameraName = photoDto.CameraName,
                Status = photoDto.Status,
                UploadDate = photoDto.UploadDate,
                Tag = photoDto.Tag,
                UserId = new ObjectId(photoId)
            };
            _photoServiceMock.Setup(x => x.UpdatePhotoAsync(updatedPhoto)).Returns(Task.CompletedTask);

            // Act
            var result = await _photoController.UpdatePhoto(photoId, photoDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        */


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //DeletePhoto

        [Fact]
        public async Task DeletePhoto_ReturnsInternalServerErrorResult_ExceptionThrown()
        {
            // Arrange
            var photoId = "6151dc195dc68dab2c80c66d";
            var objectId = new ObjectId(photoId);
            //_photoServiceMock.Setup(x => x.DeletePhotoAsync(objectId)).ThrowsAsync(new Exception("An error occurred."));

            // Act

            //Act
            var result = await _client.DeleteFromJsonAsync<PhotoDto>($"/api/PhotoControllerMongoDb{photoId}");      //.GetFromJsonAsync<List<PhotoDto>>("/api/PhotoControllerMongoDb");

            //var result = await _photoController.DeletePhotoAsync(photoId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            //Assert.Equal(500, (result as ObjectResult).StatusCode);//!

            //Assert.Equal(6, result.Count);
        }

    }
}

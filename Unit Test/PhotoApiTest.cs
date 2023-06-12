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


       

        //FindById - X
        [Fact]
        public async Task Get_Object_Returns_Success()
        {
            // Arrange
            //int objectId = 1; // Замініть на ідентифікатор існуючого об'єкта

            // Arrange
            var validId = ObjectId.GenerateNewId();

            var photo = new Photo//Kiedy wchodzimy Object Photo wyskakuje błędy
            {
                Id = validId,//!
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
            var result  = await _client.GetAsync($"/api/PhotoControllerMongoDb/{validId}");

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


        //GetPhotosByUserId_ReturnsOkResult - x

        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly PhotoControllerMongoDb _photoController;

        public PhotoControllerTests()
        {
            _photoServiceMock = new Mock<IPhotoService>();
            _photoController = new PhotoControllerMongoDb(_photoServiceMock.Object);
        }

        [Fact]
        public async Task GetPhotosByUserId_ReturnsOkResult()
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
                },
                new Photo
                {
                    Id = new ObjectId("6151dc195dc68dab2c80c66f"),
                    Url = "https://example.com/photo2.jpg",
                    Title = "Photo 2",
                    Description = "This is photo 2",
                    CameraName = "Nikon D850",
                    Status = "Active",
                    UploadDate = DateTime.Now,
                    Tag = "Travel",
                    UserId = userId
                }
            };
            _photoServiceMock.Setup(x => x.GetPhotosByUserIdAsync(userId)).ReturnsAsync(photos);

            // Act
            var result = await _photoController.FindByUserId(userId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*

            [Fact]
            public async Task GetPhotoByIdAsync_WithValidId_ReturnsPhotoDto()
            {
                // Arrange
                var validId = ObjectId.GenerateNewId();

                var photo = new Photo//Kiedy wchodzimy Object Photo wyskakuje błędy
                {
                    Id = validId,//!
                    Url = "https://example.com/photo.jpg",
                    Title = "Sample Photo",
                    Description = "This is a sample photo",
                    CameraName = "Nikon",
                    Status = "Active",
                    UploadDate = DateTime.UtcNow,
                    Tag = "Sample",
                    UserId = ObjectId.GenerateNewId()//!
                };

                var mockService = new Mock<PhotoServiceMongoDb>();
                mockService.Setup(s => s.GetPhotoByIdAsync(validId)).ReturnsAsync(photo);

                var controller = new PhotoControllerMongoDb(mockService.Object);//PhotoController

                // Act
                var result = await controller.FindById(validId);

                // Assert
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
            }
            

        */

        //dont work
        /*
        [Fact]
        public async Task GetPhotoByIdAsync_WithValidId_ReturnsPhoto()
        {
            // Arrange
            var validId = 1;

            // Act
            var result = await _client.GetAsync($"/api/PhotoControllerMongoDb/{validId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            var photo = JsonConvert.DeserializeObject<PhotoDto>(content);

            Assert.NotNull(photo);
            Assert.Equal(validId.ToString(), photo.Id);
        }
        */






        //?
        /*
        [Fact]
        public async void PostTest()
        {

            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:7077/api/books"),
                Method = HttpMethod.Post,
                Headers =
                    {
                        {HttpRequestHeader.Authorization.ToString(), "Bearer 3789..."},
                        {HttpRequestHeader.ContentType.ToString(), "application/json"}
                    },
                Content = JsonContent.Create(body),
            };

            var response = await _client.SendAsync(request);

        }
        */

    }
}

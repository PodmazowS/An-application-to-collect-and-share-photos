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



        [Fact]//it's correct        
        public async void GetShouldReturnCountPhotos()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetFromJsonAsync<List<PhotoDto>>("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(5, result.Count);//Equal(9, -   count photos in Db
        }




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

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
    public class AlbumApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        //Arrange for all methods 
        private readonly HttpClient _client;

        public AlbumApiTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]//it's correct        
        public async void GetShouldReturnCountAlbums()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetFromJsonAsync<List<AlbumDto>>("/api/AlbumControllerMongoDb"); //change linq for API MongoDB

            //Assert
            Assert.Equal(15, result.Count);//Equal(9, -   count photos in Db
        }


        [Fact]//it's correct
        public async void GetShouldReturnOkStatusForAlbum()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetAsync("/api/AlbumControllerMongoDb");

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

            var album = new Album//Kiedy wchodzimy Object Photo wyskakuje błędy
            {
                Id = id,//!
                Title = "Testing",
                Status = "public",
                UserId = ObjectId.GenerateNewId(),//!
                Description = "Testing"
            };

            // Act
            var result = await _client.GetAsync($"/api/AlbumControllerMongoDb/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var albumDto = Assert.IsType<AlbumDto>(okResult.Value);

            Assert.Equal(album.Id.ToString(), albumDto.Id);
            Assert.Equal(album.Title, albumDto.Title);
            Assert.Equal(album.Status, albumDto.Status);
            Assert.Equal(album.Description, albumDto.Description);
            Assert.Equal(album.UserId.ToString(), albumDto.UserId);
            //Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //UpdatePhoto

        /*
        [Fact]
        public async Task UpdateAlbum_ReturnsOkResult()
        {
            // Arrange
            var id = "6151dc195dc68dab2c80c66d";
             var album = new Album//Kiedy wchodzimy Object Photo wyskakuje błędy
            {
                Id = id,//!
                Title = "Testing",
                Status = "public",
                UserId = ObjectId.GenerateNewId(),//!
                Description = "Testing"
            };
            var updatedPhoto = new Photo
            {
                Id = new ObjectId(photoId),

                Id = new ObjectId(id),
                Title = albumDto.title,
                Status = albumDto.status,
                UserId = new ObjectId(id),
                Description = albumDto.description

                
            };
            _photoServiceMock.Setup(x => x.UpdatePhotoAsync(updatedPhoto)).Returns(Task.CompletedTask);

            // Act
            var result = await _photoController.UpdatePhoto(photoId, photoDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        */
    }
}

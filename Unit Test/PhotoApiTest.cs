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
            Assert.Equal(12, result.Count);//Equal(9, -   count photos in Db
        }



        [Fact]//it's correct
        public async void GetShouldReturnOkStatus()
        {
            
            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetAsync("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }


        

        
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

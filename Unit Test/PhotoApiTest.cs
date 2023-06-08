using Infrastructure.Controller;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test
{
    public class PhotoApiTest
    {
        [Fact]
        public async void GetShouldReturnCountPhotos()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetFromJsonAsync<List<PhotoDto>>("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(9, result.Count);//Equal(18, -   count photos in Db
        }



        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetAsync("/api/PhotoControllerMongoDb");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
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

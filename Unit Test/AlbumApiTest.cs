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
    }
}

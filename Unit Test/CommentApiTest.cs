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
using Web_Api;

namespace Unit_Test
{
    //At the moment table for comment in DB is Empty!!!
    public class CommentApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        //Arrange for all methods 
        private readonly HttpClient _client;

        public CommentApiTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]//it's correct        
        public async void GetShouldReturnCountComments() 
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetFromJsonAsync<List<CommentDto>>("/api/CommentControllerMongoDb"); //change linq for API MongoDB

            //Assert
            Assert.Equal(5, result.Count);//Equal(9, -   count photos in Db
        }


        [Fact]//it's correct
        public async void GetShouldReturnOkStatusForComment()
        {

            //await using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //Act
            var result = await _client.GetAsync("/api/CommentControllerMongoDb");

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
            var photoid = ObjectId.GenerateNewId();

            var comment = new Comment//Kiedy wchodzimy Object Photo wyskakuje błędy
            {
                Id = photoid,//!
                PhotoId = ObjectId.GenerateNewId(),//!
                UserId = ObjectId.GenerateNewId(),//!
                CommentText = "Testing",
                Date = DateTime.Now
            };

            // Act
            var result = await _client.GetAsync($"/api/PhotoControllerMongoDb/{photoid}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var commentDto = Assert.IsType<CommentDto>(okResult.Value);

            Assert.Equal(comment.Id.ToString(), commentDto.Id);
            Assert.Equal(comment.UserId.ToString(), commentDto.UserId);
            Assert.Equal(comment.PhotoId.ToString(), commentDto.PhotoId);
            Assert.Equal(comment.CommentText, commentDto.CommentText);
            Assert.Equal(comment.Date, commentDto.Date);
            //Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());

        }

    }
}

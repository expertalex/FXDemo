using System;
using System.Net;
using System.Net.Http;
using FXDemo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace FXTest
{
    public class PlayerIntegrationTest
    {

        // TODO: Implement Tests for each actions.... 

        private readonly HttpClient _client;

        public PlayerIntegrationTest() {

            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            _client = server.CreateClient();



        }

        [Fact]
        public void GetAll()
        {
            var resquest = new HttpRequestMessage(new HttpMethod("GET"), "/api/player/");

            var responce = _client.SendAsync(resquest).Result;

            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);


            // TODO: More tests... 
        }
    }
}

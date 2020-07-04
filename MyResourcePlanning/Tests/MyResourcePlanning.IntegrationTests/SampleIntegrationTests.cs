namespace MyResourcePlanning.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using MyResourcePlanning.Web;
    using NUnit.Framework;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class SampleIntegrationTests
    {
        [SetUp]
        public void ApiSetUp()
        {
            this.server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Integration")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", false, true);
                })
                .UseStartup<Startup>());
            this.client = this.server.CreateClient();
            this.client.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        private TestServer server;

        private HttpClient client;

        [Test]
        [Category("IntegrationTest")]
        public async Task ApiUnauthorizedRequestToHomePage_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            var response = await this.client.SendAsync(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        [Category("IntegrationTest")]
        public async Task ApiUnauthorizedRequestToRequest_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Request/ResourceRequests");
            var response = await this.client.SendAsync(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Redirect);
        }

        [Test]
        [Category("IntegrationTest")]
        public async Task ApiAuthorizedRequestToRequest_ShouldReturnsOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Request/ResourceRequests");
            var response = await this.client.SendAsync(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
namespace MyResourcePlanning.IntegrationTests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class SampleTests : BaseTest
    {
        [Test]
        [Category("IntegrationTest")]
        public async Task ApiUnauthorizedRequestToHomePage_ShouldReturnOK()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/");
            var response = await this.Client.SendAsync(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        [Category("IntegrationTest")]
        public async Task ApiUnauthorizedRequestToRequest_ShouldBeRedirectedToLogin()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Request/ResourceRequests");
            var response = await this.Client.SendAsync(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Redirect);
            Assert.That(response.Headers.Location.OriginalString.StartsWith("http://localhost/Identity/Account/Login"));
        }

        [Test]
        [Category("IntegrationTest")]
        public async Task ApiRegisterNewUser_ShouldReturnsOK()
        {

            var initialResponse = await this.Client.GetAsync("/Identity/Account/Register");

            var antiForgeryCookieValue = ExtractAntiForgeryCookieValueFrom(initialResponse);

            var antiForgeryToken = ExtractAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var request = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Register");

            request.Headers.Add("Cookie",
                new CookieHeaderValue(AntiForgeryCookieName, antiForgeryCookieValue).ToString());

            var user = new Dictionary<string, string>()
            {
                {AntiForgeryFieldName, antiForgeryToken },
                { "FirstName", "Test" },
                { "LastName", "Test" },
                { "Email", "test@test" },
                { "PhoneNumber", "123" },
                { "Password", "Test" },
                { "ConfirmPassword", "Test" },
            };

            request.Content = new FormUrlEncodedContent(user);

            var response = await this.Client.SendAsync(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Redirect);
            Assert.AreEqual(response.Headers.Location.OriginalString, "/");

        }
    }
}

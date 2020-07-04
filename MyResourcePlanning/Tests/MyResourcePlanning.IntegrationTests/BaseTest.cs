namespace MyResourcePlanning.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using MyResourcePlanning.Web;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    public class BaseTest
    {
        [SetUp]
        public void ApiSetUp()
        {
            this.Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Integration")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", false, true);
                })
                .UseStartup<Startup>()
                .ConfigureServices(c =>
                {
                    c.AddAntiforgery(t =>
                    {
                        t.CookieName = AntiForgeryCookieName;
                        t.FormFieldName = AntiForgeryFieldName;
                    });
                })
                );

            this.Client = this.Server.CreateClient();
            this.Client.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        public TestServer Server { get; set; }

        public HttpClient Client { get; set; }

        protected const string AntiForgeryFieldName = "_AFTField";
        protected const string AntiForgeryCookieName = "AFTCookie";

        protected static string ExtractAntiForgeryCookieValueFrom(HttpResponseMessage response)
        {
            string antiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(x => x.Contains(AntiForgeryCookieName));

            if (antiForgeryCookie is null)
            {
                throw new ArgumentException($"Cookie '{AntiForgeryCookieName}' not found in HTTP response", nameof(response));
            }

            string antiForgeryCookieValue = SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();

            return antiForgeryCookieValue;
        }

        protected static string ExtractAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' not found in HTML", nameof(htmlBody));
        }
    }
}
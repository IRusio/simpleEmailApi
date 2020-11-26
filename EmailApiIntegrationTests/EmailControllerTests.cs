using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmailApi;
using EmailApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace EmailApiIntegrationTests
{
    public class EmailControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public EmailControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task EmailGetHistoryAsync()
        {

            var response = await _client.GetAsync("Email");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("test@test.test", "adam")]
        [InlineData("test@test.test", "pawel")]
        public async Task AddTemplateAndSendEmailAsync(string email, string name)
        {
            var templates = await _client.GetAsync("/EmailTemplates").ConfigureAwait(false);
            templates.EnsureSuccessStatusCode();
            var template = JsonConvert.DeserializeObject<List<Template>>(await templates.Content.ReadAsStringAsync());
            var xd = templates.Content;
            var request = new HttpRequestMessage(HttpMethod.Post, "/Email")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                    new EmailWithName() {EmailAddress = email, TemplateId = template.Single().Id, Name = name}
                ), Encoding.UTF8, "application/json")
            };
            //await _client.PostAsync()
            var response = await _client.SendAsync(request);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
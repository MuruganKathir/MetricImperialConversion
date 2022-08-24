using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Conversion.API.Tests.ApiTests
{
    [TestFixture]
    public class TemperatureControllerTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void StartupTest()
        {
            var factory = new TestFactory();
            TestServer server = factory.Server;

            _httpClient = server.CreateClient();
        }

        [Test]
        public async Task Can_Call_ConvertToCelsius()
        {
            var response = await _httpClient.GetAsync("api/temperature/celsius/90");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 32.2222m);
        }

        [Test]
        public async Task Can_Call_ConvertToFahrenheit()
        {
            var response = await _httpClient.GetAsync("api/temperature/fahrenheit/32.2222");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 90);
        }
    }
}

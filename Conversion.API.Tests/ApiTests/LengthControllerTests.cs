using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Conversion.API.Tests.ApiTests
{
    [TestFixture]
    public class LengthControllerTests
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
        public async Task Can_Call_ConvertToKilometers()
        {
            var response = await _httpClient.GetAsync("api/length/kilometers/4");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 6.4374m);
        }

        [Test]
        public async Task Can_Call_ConvertToMiles()
        {
            var response = await _httpClient.GetAsync("api/length/miles/2");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 1.2427m);
        }
    }
}
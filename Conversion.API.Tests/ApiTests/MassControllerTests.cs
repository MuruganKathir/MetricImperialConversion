using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Conversion.API.Tests.ApiTests
{
    [TestFixture]
    public class MassControllerTests
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
        public async Task Can_Call_ConvertToKilograms()
        {
            var response = await _httpClient.GetAsync("api/mass/kilograms/225");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 102.06m);
        }

        [Test]
        public async Task Can_Call_ConvertToPounds()
        {
            var response = await _httpClient.GetAsync("api/mass/pounds/100");
            var result = await response.Deserialize<decimal>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(result == 225);
        }
    }
}
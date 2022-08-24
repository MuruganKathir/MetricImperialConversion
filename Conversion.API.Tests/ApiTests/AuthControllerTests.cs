using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Conversion.Core.ApiModels;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Conversion.API.Tests.ApiTests
{
    [TestFixture]
    public class AuthControllerTests
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
        public async Task Can_Register_New_User()
        {
            var newUser = new RegisterRequest();
            newUser.Username = "Tester" + new Random().Next(1, 10000);
            newUser.Email = newUser.Username + "@test.com";
            newUser.Password = newUser.Username + "!";

            var response = await _httpClient.PostAsJsonAsync("auth/register", newUser);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Can_Login_User()
        {
            var loginRequest = new LoginRequest {Username = "Tester3260", Password = "Tester3260!" };

            var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);
            var result = await response.Deserialize<object>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(result);
        }
    }
}
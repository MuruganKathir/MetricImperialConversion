using System.IO;
using Microsoft.Extensions.Configuration;

namespace Conversion.API.Tests
{
    public class TestApiStartup : Startup
    {
        public new static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public TestApiStartup() : base(Configuration)
        {
        }

       
    }
}
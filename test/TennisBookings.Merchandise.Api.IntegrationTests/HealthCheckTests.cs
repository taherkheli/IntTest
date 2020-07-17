using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests
{
	public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly HttpClient _httpClient;

		public HealthCheckTests(WebApplicationFactory<Startup> factory)
		{
			_httpClient = factory.CreateDefaultClient();
		}

		[Fact]
		public async Task HealthCheck_ReturnsOk()
		{
			var response = await _httpClient.GetAsync("/healthcheck");

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			//response.EnsureSuccessStatusCode();   //a shortcut if we dun care about the exact code (200) and care for a range only (200-299)
		}
	}
}

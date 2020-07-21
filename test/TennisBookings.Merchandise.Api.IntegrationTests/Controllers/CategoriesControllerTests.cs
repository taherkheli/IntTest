using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
	public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly HttpClient _httpClient;

		public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
		{
			//_httpClient = factory.CreateDefaultClient(new Uri("http://localhost/api/categories"));

			factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/categories");
			_httpClient = factory.CreateClient();
		}

		[Fact]
		public async Task GetAll_ReturnsExpectedResponse()
		{
			var expected = new List<string> { "Accessories", "Bags", "Balls", "Clothing", "Rackets" };

			//now we implicitly deserialize the received json into a C# object <ExpectedCategoriesModel>
			//we also do all the checks that we were doin in different tests e.g. json content type, success code etc.
			var model = await _httpClient.GetFromJsonAsync<ExpectedCategoriesModel>("");

			Assert.NotNull(model?.AllowedCategories);
			Assert.Equal(expected.OrderBy(s => s), model.AllowedCategories.OrderBy(s => s));
		}

		[Fact]
		public async Task GetAll_SetsExpectedCacheControlHeader()
		{
			var response = await _httpClient.GetAsync("");
			var header = response.Headers.CacheControl;

			Assert.True(header.MaxAge.HasValue);
			Assert.Equal(TimeSpan.FromMinutes(5), header.MaxAge);
			Assert.True(header.Public);
		}
	}
}

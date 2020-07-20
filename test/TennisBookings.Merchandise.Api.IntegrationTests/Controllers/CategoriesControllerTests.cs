using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api.IntegrationTests.Helpers.Serialization;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
	public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly HttpClient _httpClient;

		public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
		{
			_httpClient = factory.CreateDefaultClient();
		}

		[Fact]
		public async Task GetAll_ReturnsSuccessStatusCode()
		{
			var response = await _httpClient.GetAsync("/api/categories");

			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task GetAll_ReturnsExpectedMediaType()
		{
			var response = await _httpClient.GetAsync("/api/categories");

			Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
		}

		[Fact]
		public async Task GetAll_ReturnsContent()
		{
			var response = await _httpClient.GetAsync("/api/categories");

			Assert.NotNull(response.Content);
			Assert.True(response.Content.Headers.ContentLength > 0);
		}

		//[Fact]
		//public async Task GetAll_ReturnsExpectedJson()
		//{
		//	var response = await _httpClient.GetStringAsync("/api/categories");

		//	Assert.Equal("{\"allowedCategories\":[\"Accessories\",\"Bags\",\"Balls\",\"Clothing\",\"Rackets\"]}", response);
		//}

		[Fact]
		public async Task GetAll_ReturnsExpectedJson()
		{
			var expected = new List<String> { "Accessories", "Bags", "Balls", "Clothing", "Rackets" };

			var responseStream = await _httpClient.GetStreamAsync("/api/categories");

			var model = await JsonSerializer.DeserializeAsync<ExpectedCategoriesModel>(responseStream, JsonSerializerHelper.DefaultDeserialisationOptions);

			//null conditional operator '?.' checks that the main object (model) is not null n its prop (AllowedCategories) is not null either 
			Assert.NotNull(model?.AllowedCategories);

			//order alphabetically
			Assert.Equal(expected.OrderBy ( s => s), model.AllowedCategories.OrderBy( s => s));
		}
	}
}

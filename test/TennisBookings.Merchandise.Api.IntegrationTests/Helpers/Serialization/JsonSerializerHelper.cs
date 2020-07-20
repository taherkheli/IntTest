﻿using System.Text.Json;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Helpers.Serialization
{
	public class JsonSerializerHelper
	{
		public static JsonSerializerOptions DefaultSerialisationOptions => new JsonSerializerOptions
		{
			IgnoreNullValues = true
		};

		public static JsonSerializerOptions DefaultDeserialisationOptions => new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};
	}
}

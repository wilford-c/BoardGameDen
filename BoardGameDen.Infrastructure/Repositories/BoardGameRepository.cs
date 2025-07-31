using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BoardGameDen.Application.Interfaces;
using BoardGameDen.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace BoardGameDen.Infrastructure.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        //private readonly ILogger _logger;
        private JsonSerializerOptions? _jsonOptions;
        private const string CacheKey = "BoardGames";
        private const string ApiUrl = "https://myboardgamelibrary.com/boardgames.json";

        public BoardGameRepository(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            //_logger = logger;
        }



        public async Task<(List<BoardGame> BoardGames, int TotalCount)> GetBoardGamesAsync(string search, int page, int pageSize)
        {
           
            if (_cache.TryGetValue(CacheKey, out object? cachedObj) && cachedObj is ValueTuple<List<BoardGame>, int> cachedTuple)
            {
                return cachedTuple;
            }

            var response = await _httpClient.GetFromJsonAsync<BoardGamesApiResponse>(ApiUrl);
            var httpResponse = await _httpClient.GetAsync(ApiUrl);
            if (!httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"HTTP error {httpResponse.StatusCode} when calling API: {httpResponse.ReasonPhrase}");
                //_logger?.LogError("HTTP error {StatusCode} when calling API: {Reason}", httpResponse.StatusCode, httpResponse.ReasonPhrase);
                return (new List<BoardGame>(), 0);
            }

            var json = await httpResponse.Content.ReadAsStringAsync();
            //_logger?.LogInformation("Received JSON: {Json}", json);

            try
            {
                response = JsonSerializer.Deserialize<BoardGamesApiResponse>(json, _jsonOptions);
                if (response == null)
                    return (new List<BoardGame>(), 0);

                var gamelist = response.BoardGames ?? new List<BoardGame>();
                var count = response.TotalCount;

                _cache.Set(CacheKey, (gamelist, count), TimeSpan.FromHours(1));
                

                return (gamelist, response.TotalCount);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
                //_logger?.LogError(ex, "JSON deserialization failed");
                return (new List<BoardGame>(), 0);
            }



        }



    }
}

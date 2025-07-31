using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BoardGameDen.Domain.Entities;

namespace BoardGameDen.Infrastructure.Repositories
{
    public class BoardGamesApiResponse
    {
        public int TotalCount { get; set; }

        [JsonPropertyName("Sheet1")]
        public List<BoardGame>? BoardGames { get; set; }
    }
}

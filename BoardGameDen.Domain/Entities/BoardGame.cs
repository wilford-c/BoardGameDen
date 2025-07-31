using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BoardGameDen.Domain.Entities
{
    public class BoardGame
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("MinPlayers")]
        public int MinPlayers { get; set; }

        [JsonPropertyName("MaxPlayers")]
        public int MaxPlayers { get; set; }

        [JsonPropertyName("MinTime")]
        public int MinTime { get; set; }

        [JsonPropertyName("MaxTime")]
        public int MaxTime { get; set; }

        [JsonPropertyName("BGGRating")]
        public double BGGRating { get; set; }

        [JsonPropertyName("URL")]
        public string URL { get; set; }

        [JsonPropertyName("Thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("Thumbnail2")]
        public string Thumbnail2 { get; set; }

        [JsonPropertyName("MainImage")]
        public string MainImage { get; set; }

        [JsonPropertyName("SalePrice")]
        public decimal SalePrice { get; set; }

    }


        
}

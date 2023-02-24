using System;
using System.Text.Json.Serialization;

namespace TodoApi.DTOs
{
    public class TradeDto
    {
        public int? Id { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        public string Symbol { get; set; }

        public int Shares { get; set; }

        public int? Price { get; set; }

        public long? Timestamp { get; set; }
    }
}

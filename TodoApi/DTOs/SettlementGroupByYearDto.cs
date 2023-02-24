using System;
using System.Text.Json.Serialization;

namespace TodoApi.DTOs
{
    public class SettlementGroupByYearDto
    {
        [JsonPropertyName("settlement_location")]
        public string SettlemenLocation { get; set; }

        [JsonPropertyName("year_date")]
        public DateTime YearDate { get; set; }

        [JsonPropertyName("avg_price")]
        public double AveragePrice { get; set; }

        [JsonPropertyName("avg_volume")]
        public double AverageVolume { get; set; }

        [JsonPropertyName("avg_total_dollars")]
        public double AverageTotalDollars { get; set; }

        [JsonPropertyName("min_price")]
        public double MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public double MaxPrice { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;

namespace CurrencyQuoteWorkerService.Shared.DTOs
{
    public class CurrencyByDateDTO
    {
        [JsonPropertyName("moeda")]
        public string Currency { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_fim")]
        public DateTime EndDate { get; set; }
    }
}

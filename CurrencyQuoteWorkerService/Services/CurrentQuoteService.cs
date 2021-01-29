using CurrencyQuoteWorkerService.Shared.DTOs;
using System.Net.Http;
using System.Text.Json;

namespace CurrencyQuoteWorker.Background.Services
{
    public class CurrentQuoteService
    {
        private readonly HttpClient client;
        public CurrentQuoteService()
        {
            client = new HttpClient();
        }

        public CurrencyByDateDTO[] GetNextFilters()
        {
            var response = client.GetAsync("http://localhost:5000" +
                "/api/queue/next").Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<CurrencyByDateDTO[]>(content);

            return result;
        }
    }
}

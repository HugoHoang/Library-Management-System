using Newtonsoft.Json;
using System.Text;

namespace LoanService.SyncDataServices.Http
{
    public class HttpBookDataClient : IBookDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public HttpBookDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<int> GetBookCountAsync(int bookId)
        {
            Console.WriteLine($"URL: {_configuration["BookService:BaseUrl"]}/books/{bookId}/available-count");
            var response = await _httpClient.GetAsync($"{_configuration["BookService:BaseUrl"]}/books/{bookId}/available-count");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var availableCountResponse = JsonConvert.DeserializeObject<AvailableCountResponse>(content);
            
            if (availableCountResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response from book service.");
            }

            return availableCountResponse.AvailableCount;
        }

        public async Task UpdateBookCount(int bookId, int AvailableCount)
        {
            Console.WriteLine($"URL: {_configuration["BookService:BaseUrl"]}/books/{bookId}");
            var data = new { bookId, AvailableCount };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_configuration["BookService:BaseUrl"]}/books/{bookId}", content);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Added book count");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        private class AvailableCountResponse
        {
            public int AvailableCount { get; set; }
        }
    }
    
}

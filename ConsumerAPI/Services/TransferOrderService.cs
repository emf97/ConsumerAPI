using ConsumerAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerAPI.Services
{
    public class TransferOrderService
    {
        private readonly HttpClient _httpClient;

        public TransferOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TransferOrder>> GetTransferOrdersAsync(string baseUrl)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}/TransferOrders");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Ignorar diferenciação de maiúsculas e minúsculas

            };
            return JsonSerializer.Deserialize<List<TransferOrder>>(responseBody, options);
        }
    }
}

using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiRequest;
using MinervaCyclingMobileApp.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Services
{
    public class CreateInsurranceOrderService : ICreateInsuranceOrderService
    {
        private readonly string _apiEndpoint = "YOUR_API_ENDPOINT_HERE"; // replace with actual API endpoint
        public async Task<InsurranceOrderResponse> CreateInsurranceOrder(InsurranceOrderRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = JsonSerializer.Serialize(request);
                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiEndpoint + "/Shop/CreateInsurranceOrder", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var orderResponse = JsonSerializer.Deserialize<InsurranceOrderResponse>(responseBody);
                    return orderResponse;
                }
                else
                {
                    // TODO: Handle different error responses based on status code, i.e. 400, 500.
                    throw new InvalidOperationException($"API Call Failed with status {response.StatusCode}");
                }
            }
        }
    }
}

using MinervaCyclingMobileApp.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace MinervaCyclingMobileApp.Services
{
    public class ApiManager : IApiManager
    {
        private string baseUrl = "http://192.168.1.7:5027/";
        HttpClient _client = new HttpClient();

        

        public ApiManager()
        {
            _client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<T> PostForResponseAsync<T>(string uriRequest, StringContent content, CancellationToken cancellationToken = default)
        {
            T response = default(T);

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                
                HttpResponseMessage httpResponse = await _client.PostAsync(uriRequest, content, cancellationToken);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var res = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Ooops", "There is a problem with your internet connection", "Ok");
            }

            return response;
        }

        public async Task<T> SendForResponseAsync<T>(string uriRequest, HttpMethod method, StringContent content, CancellationToken cancellationToken = default)
        {
            T response = default(T);

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                var request = new HttpRequestMessage(method, uriRequest) { Content = content };
                HttpResponseMessage httpResponse = await _client.SendAsync(request, cancellationToken);

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    await App.Current.MainPage.DisplayAlert("Ooops", $"Request failed with status code: {httpResponse.StatusCode} and reason: {httpResponse.ReasonPhrase}", "Ok");
                    Console.WriteLine(httpResponse.StatusCode);
                }

                if (httpResponse.IsSuccessStatusCode)
                {
                    var res = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Ooops", "There is a problem with your internet connection", "Ok");
            }

            return response;
        }

        public async Task<T> GetForResponseAsync<T>(string uriRequest, CancellationToken cancellationToken = default)
        {
            T response = default(T);

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                
                HttpResponseMessage httpResponse = await _client.GetAsync(uriRequest, cancellationToken);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var res = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<T>(res);
                }

                if (httpResponse.IsSuccessStatusCode)
                {
                    var res = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Ooops", "There is a problem with your internet connection", "Ok");
            }

            return response;
        }
    }
}

using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiRequest;
using MinervaCyclingMobileApp.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Services
{
    public class GetCertificatesService : IGetCertificatesService
    {
        private readonly IApiManager _apiManager;

        public GetCertificatesService(IApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        public async Task<List<GetCertificatesResponse>> GetCertificates(int contactId, CancellationToken cancellationToken = default)
        {
            string uriRequest = $"/api/BikeService/GetCertificates/{contactId}";
            try
            {
                var result = await _apiManager.GetForResponseAsync<List<GetCertificatesResponse>>(uriRequest, cancellationToken);

                return result ?? new List<GetCertificatesResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await App.Current.MainPage.DisplayAlert("Sorry", $"There is an error: {ex.Message}", "Ok");

                return new List<GetCertificatesResponse>();

            }
        }
    }
}

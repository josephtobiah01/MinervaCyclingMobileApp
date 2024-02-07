using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Models.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaCyclingMobileApp.Services
{
    public class GetWarrantiesService : IGetWarrantiesService
    {
        private readonly IApiManager _apiManager;

        public GetWarrantiesService(IApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        public async Task<List<GetWarrantiesResponse>> GetWarranties()
        {
            try
            {
                var result = await _apiManager.GetForResponseAsync<List<GetWarrantiesResponse>>("/api/BikeService/GetWarranties");

                return result ?? new List<GetWarrantiesResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await App.Current.MainPage.DisplayAlert("Sorry", $"There is an error: {ex.Message}", "Ok");

                return new List<GetWarrantiesResponse>();

            }
        }
    }
}

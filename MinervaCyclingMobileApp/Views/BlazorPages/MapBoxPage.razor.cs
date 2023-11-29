using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MinervaCyclingMobileApp.Interfaces;
using MinervaCyclingMobileApp.Services;
using System.Threading;
using Microsoft.Maui.Devices.Sensors;

namespace MinervaCyclingMobileApp.Views.BlazorPages
{
    public partial class MapBoxPage : INotifyPropertyChanged
    {
        #region Fields


        private static string _mapboxAccessToken = "pk.eyJ1Ijoiam9zZXBoLW1pbmVydmFjeWNsaW5nIiwiYSI6ImNsbm9wMjZqdDBkeGUya3F0czU0YTd0emoifQ.zyhJrLnJOmTyNkCbD-qVyg";
        private double _latitude;
        private double _longitude;
        private string _mapMarkerIcon;
        private Timer _locationTimer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(1);
        private CancellationTokenSource _cancellationTokenSource;

        #endregion Fields

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public double Latitude
        {
            get { return _latitude; }
            set { SetPropertyValue(ref _latitude, value); }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { SetPropertyValue(ref _longitude, value); }
        }

        public string MapMarkerIcon
        {
            get { return _mapMarkerIcon; }
            set { SetPropertyValue(ref _mapMarkerIcon, value); }
        }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IGeoLocationService _geoLocationService { get; set; }

        public Command GoBack { get; set; }

        #endregion Properties

        #region Constructor



        public MapBoxPage()
        {

            GoBack = new Command(GoBackCommand);

        }

        #endregion Constructor

        #region Methods

        protected bool SetPropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }

        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private void GoBackCommand(object obj)
        {
            throw new NotImplementedException();
        }


        protected override async Task OnInitializedAsync()
        {
            await GetCurrentLocationAsync();

            _cancellationTokenSource = new CancellationTokenSource();

            _geoLocationService.LocationChanged += _geoLocationService_LocationChanged;

            await _geoLocationService.StartListeningAsync(_cancellationTokenSource.Token);

        }

        private async void _geoLocationService_LocationChanged(Location location)
        {
            Console.WriteLine($"Location updated: {location.Longitude}, {location.Latitude}");

            if (location != null)
            {
                Longitude = location.Longitude;
                Latitude = location.Latitude;
                await UpdateMap(Longitude, Latitude);
            }
            
        }


        private async Task GetCurrentLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMilliseconds(3000));
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));


                if (location != null)
                {

                    Longitude = location.Longitude;

                    Latitude = location.Latitude;

                    await InitializeMap(Longitude, Latitude);
                }
            }
            catch (PermissionException)
            {
                await App.Current.MainPage.DisplayAlert("Permission Denied", "Location permission is required for this feature", "OK");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Geolocation is not supported on this device, error message: {fnsEx.Message}", "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Please enable Geolocation on this device, error message: {fneEx.Message}", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

       


        public async Task InitializeMap(double longitude, double latitude)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("initializeMap", longitude, latitude);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ooops", $"Error initializing map: {ex.Message}", "Ok");
            }
        }

        public async Task UpdateMap(double longitude, double latitude)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("updateMapMarkerPosition", longitude, latitude);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ooops", $"Error updating map marker position: {ex.Message}", "Ok");
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _geoLocationService.LocationChanged -= _geoLocationService_LocationChanged;
        } 
    }

        #endregion Methods
    
}

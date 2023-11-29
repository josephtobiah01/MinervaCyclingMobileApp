using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Messaging;
using MinervaCyclingMobileApp.Models;

namespace MinervaCyclingMobileApp.Views.Map;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetCurrentLocationAsync();
    }


    private async Task GetCurrentLocationAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                // Pass the location coordinates to the Blazor component or JavaScript method
                var message = new LocationMessage { Latitude = location.Latitude, Longitude = location.Longitude };

                WeakReferenceMessenger.Default.Send(message);

            }
        }
        catch (PermissionException)
        {
            await DisplayAlert("Permission Denied", "Location permission is required for this feature", "OK");
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            await DisplayAlert("Error", $"Geolocation is not supported on this device, error message: {fnsEx.Message}", "OK");
        }
        catch (FeatureNotEnabledException fneEx)
        {
            await DisplayAlert("Error", $"Please enable Geolocation on this device, error message: {fneEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        
    }
}
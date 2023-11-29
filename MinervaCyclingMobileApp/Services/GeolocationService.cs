using MinervaCyclingMobileApp.Interfaces;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Internals;
using System.Text.Json;
using MinervaCyclingMobileApp.Models;

namespace MinervaCyclingMobileApp.Services
{
    public class GeolocationService : IGeoLocationService
    {
        public event Action<Location> LocationChanged;

        public async Task<Location> SnapToRoadAsync(Location location)
        {
            var httpClient = new HttpClient();
            string accessToken = "pk.eyJ1Ijoiam9zZXBoLW1pbmVydmFjeWNsaW5nIiwiYSI6ImNsbm9wMjZqdDBkeGUya3F0czU0YTd0emoifQ.zyhJrLnJOmTyNkCbD-qVyg";
            string coordinates = $"{location.Longitude},{location.Latitude}";
            string requestUrl = $"https://api.mapbox.com/directions/v5/mapbox/driving/{coordinates};{coordinates}?access_token={accessToken}&geometries=geojson&overview=full&steps=true";

            try
            {
                var response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var directionsResponse = JsonSerializer.Deserialize<DirectionsResponse>(json);
                    var snappedLocation = directionsResponse.Waypoints?.FirstOrDefault()?.Location;
                    if (snappedLocation != null && snappedLocation.Length >= 2)
                    {
                        return new Location
                        {
                            Longitude = snappedLocation[0],
                            Latitude = snappedLocation[1],
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", $"Error during Map Matching, {ex.Message}", "Ok");
            }

            return location;
        }

        //public async Task<Location> GetMapMatchedLocation(Location location)
        //{
        //    var httpClient = new HttpClient();
        //    string accessToken = "pk.eyJ1Ijoiam9zZXBoLW1pbmVydmFjeWNsaW5nIiwiYSI6ImNsbm9wMjZqdDBkeGUya3F0czU0YTd0emoifQ.zyhJrLnJOmTyNkCbD-qVyg";
        //    string coordinates = $"[{location.Longitude}, {location.Latitude}]";
        //    string requestUrl = $"https://api.mapbox.com/matching/v5/mapbox/driving/{coordinates}?access_token={accessToken}";

        //    try
        //    {
        //        var response = await httpClient.GetAsync(requestUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseBody = await response.Content.ReadAsStringAsync();
        //            var mapMatchResponse = JsonSerializer.Deserialize<MapMatchResponse>(responseBody);
        //            var tracePoint = mapMatchResponse.Tracepoints.FirstOrDefault();
        //            if(tracePoint != null && tracePoint.Location.Count >= 2)
        //            {
        //                return new Location
        //                {
        //                    Longitude = tracePoint.Location[0],
        //                    Latitude = tracePoint.Location[1],
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Oops", $"Error during Map Matching, {ex.Message}", "Ok");
        //    }

        //    return location;
        //}
        public async Task StartListeningAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMilliseconds(3000));
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        LocationChanged?.Invoke(location);
                    }
                    

                }
                catch(Exception ex)
                {
                   await App.Current.MainPage.DisplayAlert("Ooops", $"{ex.Message}", "OK");
                }

                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}

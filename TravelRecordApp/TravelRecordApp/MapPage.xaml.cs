using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace TravelRecordApp
{
    public partial class MapPage : ContentPage
	{
        IGeolocator locator = CrossGeolocator.Current;

        public MapPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			GetLocation();

            GetPosts();
        }

        private async void GetPosts()
        {
            //using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            //{
            //    conn.CreateTable<Post>();
            //    var posts = conn.Table<Post>().ToList();

            //    DisplayOnMap(posts);
            //}

            var posts = await Firestore.Read();
            DisplayOnMap(posts);
        }

        private void DisplayOnMap(List<Post> posts)
        {
            foreach(var post in posts)
            {
                try
                {
                    var pinPosition = new Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Position = pinPosition,
                        Label = post.VenueName,
                        Address = post.Address,
                        Type = PinType.SavedPin
                    };

                    locationMaps.Pins.Add(pin);
                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            locator.StopListeningAsync();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                if (!locator.IsListening) // Needs to verify if locator is listening. If this statement is not there, it will crash app.
                {
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);
                }

                locationMaps.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
            Xamarin.Forms.Maps.MapSpan span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);

            locationMaps.MoveToRegion(span);
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if(status == PermissionStatus.Granted)
                return status;

            if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                await DisplayAlert("Permission", "Make sure to enjoy 100% this app, you need to turn on location on settings", "OK");

                    return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}


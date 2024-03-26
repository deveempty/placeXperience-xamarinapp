using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using SQLite;
using TravelRecordApp.Helpers;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using static TravelRecordApp.Model.Venue;

namespace TravelRecordApp
{	
	public partial class NewTravelPage : ContentPage
	{	
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var selectedItem = venueListView.SelectedItem as Result;
                Post post = new Post()
                {
                    Experience = expEntry.Text,
                    VenueName = selectedItem.name,
                    Address = selectedItem.vicinity,
                    Latitude = selectedItem.geometry.location.lat,
                    Longitude = selectedItem.geometry.location.lng,
                    PlaceId = selectedItem.place_id
                  
                };

                //using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
                //{
                //    conn.CreateTable<Post>();
                //    int rows = conn.Insert(post);
                //    if (rows > 0)
                //        DisplayAlert("Success", "Experience succesfully added!", "OK");
                //    else
                //        DisplayAlert("Error", "Experience not added!", "OK");

                //}

                bool result = Firestore.Insert(post);
                if (result)
                {
                    expEntry.Text = string.Empty;
                    DisplayAlert("Success", "Experience succesfully added!", "OK");
                    Navigation.PushAsync(new HomePage());
                }   
                else
                    DisplayAlert("Error", "Experience not added!", "OK");
            }
            catch (NullReferenceException nre)
            {

            }
            catch(Exception ex)
            {
                
            }
            

        }
    }
}


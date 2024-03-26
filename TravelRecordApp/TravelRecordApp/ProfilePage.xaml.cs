using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using TravelRecordApp.Helpers;

namespace TravelRecordApp
{	
	public partial class ProfilePage : ContentPage
	{	
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using(SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            //{
            //	var postTable = conn.Table<Post>().ToList();

            var postTable = await Firestore.Read();

            var placeIds = (from p in postTable
                            orderby p.PlaceId
                            select p.PlaceId.Trim()).Distinct().ToList();

            Dictionary<string, int> placeIdCount = new Dictionary<string, int>();

            foreach (var placeid in placeIds)
            {
                var count = (from post in postTable
                             where post.PlaceId.Trim().Equals(placeid, StringComparison.OrdinalIgnoreCase)
                             select post).ToList().Count;


                var establishmentName = (from p in postTable
                                         where p.PlaceId.Trim().Equals(placeid, StringComparison.OrdinalIgnoreCase)
                                         select p.VenueName).FirstOrDefault();

                placeIdCount.Add(establishmentName, count);
            }

            categoriesListView.ItemsSource = placeIdCount;


            expCount.Text = postTable.Count.ToString();
            //}
        }

    }

	
}


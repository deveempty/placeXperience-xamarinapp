using System;
using System.Collections.Generic;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{	
	public partial class HomePage : TabbedPage
	{
        public HomePage ()
		{
			InitializeComponent ();
		}

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
			Navigation.PushAsync(new NewTravelPage());
        }

        void ToolbarItem_Clicked_DeleteAll(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            {
                conn.DeleteAll<Post>();
                int count = conn.Table<Post>().Count();
                if(count == 0)
                {
                    DisplayAlert("Success", "All post deleted!", "OK");
                    Navigation.PushAsync(new HomePage());
                }
                else
                    DisplayAlert("Error", "There was an issue deleting all post", "OK");

            }

        }
    }
}


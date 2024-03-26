using System;
using System.Collections.Generic;
using SQLite;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{	
	public partial class PostDetailPage : ContentPage
	{
        Post selectedPost;
		public PostDetailPage (Post selectedPost)
		{
			InitializeComponent ();

            this.selectedPost = selectedPost;

            expEntry.Text = selectedPost.Experience;
            expEntryId.Text = selectedPost.PlaceId;
		}

        async void UpdateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedPost.Experience = expEntry.Text;

            //using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Update(selectedPost);
            //    if (rows > 0)
            //        DisplayAlert("Success", "Experience succesfully updated!", "OK");
            //    else
            //        DisplayAlert("Error", "Experience not updated!", "OK");

            //}

            bool result = await Firestore.Update(selectedPost);
            if (result)
            {
                await DisplayAlert("Success", "Experience succesfully updated!", "OK");
                await Navigation.PopAsync();

            }
            else
                await DisplayAlert("Error", "Experience not updated!", "OK");
        }

        async void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool result = await Firestore.Delete(selectedPost);
            if (result)
            {
                await DisplayAlert("Success", "Experience succesfully deleted!", "OK");
                await Navigation.PopAsync();
            }
                
            else
               await DisplayAlert("Error", "Experience not deleted!", "OK");

            //using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Delete(selectedPost);
            //    if (rows > 0)
            //        DisplayAlert("Success", "Experience succesfully deleted!", "OK");
            //    else
            //        DisplayAlert("Error", "Experience not deleted!", "OK");

            //}

        }

    }
}


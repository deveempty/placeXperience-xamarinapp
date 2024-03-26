using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using TravelRecordApp.Helpers;

namespace TravelRecordApp
{	
	public partial class HistoryPage : ContentPage
	{	
		public HistoryPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection conn = new SQLiteConnection(App.DbLocation))
            //{
            //             conn.CreateTable<Post>();
            //             var posts = conn.Table<Post>().ToList();
            //	postListView.ItemsSource = posts;
            //         }

            var posts = await Firestore.Read();
            postListView.ItemsSource = posts;
        }

        void postListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}


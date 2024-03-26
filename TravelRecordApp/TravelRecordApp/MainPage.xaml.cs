using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            logoImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.logo.png", assembly);
        }
        private async void loginButton_Clicked(System.Object sender, System.EventArgs e)
        {

            bool isEmailEmpty = string.IsNullOrEmpty(Email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
            }
            else
            {
                //Authentication
                bool result = await Auth.LoginUser(Email.Text, Password.Text);


                if(result)
                    await Navigation.PushAsync(new HomePage());
            }
        }
    }
}


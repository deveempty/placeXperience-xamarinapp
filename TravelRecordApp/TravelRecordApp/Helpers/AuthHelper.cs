using System;
using System.Threading.Tasks;
using Xamarin.Forms;

//PLEASE NOTICE, FOR APPLE AUTHENTICATION WILL NEED KEYCHAIN SETUP USING AN DEVELOPER APPLE ACCOUNT.

namespace TravelRecordApp.Helpers
{
	public class Auth
	{

        public interface IAuth
        {
            Task<bool> RegisterUser(string email, string password);
            Task<bool> LoginUser(string email, string password);
            bool IsAuthenticatedUser();
            string GetCurrentUserId();
        }

        private static IAuth auth = DependencyService.Get<IAuth>();

		public static async Task<bool> RegisterUser(string email, string password)
		{
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }

        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                // user not exist, it will auto register it
                return await auth.LoginUser(email, password);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                string validatorCode = "400";
                string stringValidator = "Invalid Credentials";
                if (ex.Message.Contains(validatorCode) || ex.Message.Contains(stringValidator))
                    return await RegisterUser(email, password);
                return false;
            }
            
        }

        public static bool IsAuthenticatedUser()
        {
            return auth.IsAuthenticatedUser();
        }

        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}


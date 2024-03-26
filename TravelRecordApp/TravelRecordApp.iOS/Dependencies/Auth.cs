using System;
using System.Threading.Tasks;
using Foundation;
using Xamarin.Forms;
using static TravelRecordApp.Helpers.Auth;


[assembly: Dependency(typeof(TravelRecordApp.iOS.Dependencies.Auth))]
namespace TravelRecordApp.iOS.Dependencies
{
	public class Auth : IAuth
	{
		public Auth()
		{
		}

        public string GetCurrentUserId()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsAuthenticatedUser()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return true;
            }
            catch(NSErrorException error)
            {
                string message = error.Message.Substring(error.Message.IndexOf("NSLocalizedDescription="));
                message = message.Replace("NSLocalizedDescription=", "").Split('.')[0];
                throw new Exception(message);
            }
            catch(Exception ex)
            {
                throw new Exception("There was an unknow error");
            }
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                return true;
            }
            catch (NSErrorException error)
            {
                string message = error.Message.Substring(error.Message.IndexOf("NSLocalizedDescription="));
                message = message.Replace("NSLocalizedDescription=", "").Split('.')[0];
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an unknow error");
            }
        }
    }
}


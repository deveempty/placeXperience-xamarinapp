using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using static TravelRecordApp.Helpers.Auth;

[assembly: Dependency(typeof(TravelRecordApp.Droid.Dependencies.Auth))]
namespace TravelRecordApp.Droid.Dependencies
{
	public class Auth : IAuth
	{
		public Auth()
		{
		}

        public string GetCurrentUserId()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool IsAuthenticatedUser()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid != null;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch(FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                throw new Exception("400");
            }
            catch (Exception ex)
            {
                string stringValidator = "INVALID_LOGIN_CREDENTIALS";
                if (ex.Message.Contains(stringValidator))
                    throw new Exception("Invalid Credentials");
                else
                    throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an unknown error");
            }
        }
    }
}


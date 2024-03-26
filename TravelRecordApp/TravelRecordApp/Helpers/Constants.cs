using System;
using RestSharp;

namespace TravelRecordApp.Helpers
{
	public class Constants
	{
        public const int RADIUS = 2000;
        public const string GOOGLE_ENDPOINT = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&key={3}";
        public const string CLIENT_ID = "";
        public const string CLIENT_SECRET = "";
        public const string APIKEY = "";

    }
}


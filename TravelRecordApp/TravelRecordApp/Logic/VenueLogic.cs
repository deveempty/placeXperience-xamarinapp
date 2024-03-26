using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using static TravelRecordApp.Model.Venue;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Result>> GetVenues(double latitude, double longitude)
        {
            List<Result> resultGoogle = new List<Result>();

            

            var url = String.Format(Constants.GOOGLE_ENDPOINT, latitude, longitude, Constants.RADIUS, Constants.APIKEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var googleApi = JsonConvert.DeserializeObject<GoogleApi>(json);

                resultGoogle = googleApi.results as List<Result>;
            }

            return resultGoogle;
        }

    }
}

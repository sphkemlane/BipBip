using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BipBip.Services
{
    internal class VilleService
    {
        private HttpClient _client;

        public VilleService()
        {
            _client = new HttpClient();
        }

        public async Task<List<String>> ChargerVilles()
        {
            try
            {
                //url de m'api
                var response = await _client.GetAsync("https://nominatim.openstreetmap.org/search?country=France&format=json&addressdetails=1&limit=1000");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<string>>(content);
                }
                else
                {
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                return new List<string>();
            }


        }
        }
}

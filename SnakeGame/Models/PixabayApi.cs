using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SnakeGame.Models
{
    class PixabayApi
    {
        private static string PixabayApiUrl = "https://pixabay.com/api/";
        private static Configuration configuration = new Configuration();
        private HttpClient client { get; set; }

        public PixabayApi()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(PixabayApiUrl);
        }

        public PixabayRequest GetDataFromApi(string searchedObjectValue, int page = 1, int per_page = 3)
        {
            string request = $"?key={configuration.PixabyConfigKey}&q={searchedObjectValue}&page={page}&per_page={per_page}";
            HttpResponseMessage response = client.GetAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<PixabayRequest>(result);
            }

            return null;
        }


    }
}

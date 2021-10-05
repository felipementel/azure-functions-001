using System.Collections.Generic;
using System.Net.Http;
using Training.FunctionApp.TimerTrigger.Model;

namespace Training.FunctionApp.TimerTrigger
{
    public class WebRequestFelipe
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebRequestFelipe(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Posts> GetAllPosts()
        {
            using (var _client = _httpClientFactory.CreateClient("Avanade"))
            {
                var response = _client.GetAsync("/posts").Result;

                if (response.IsSuccessStatusCode)
                {
                    var itemString = response.Content.ReadAsStringAsync().Result;
                    var itemJson = System.Text.Json.JsonSerializer.Deserialize<List<Posts>>(itemString, 
                        new System.Text.Json.JsonSerializerOptions 
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    return itemJson;
                }
                else
                {
                    return new List<Posts>();
                }
            }
        }
    }
}
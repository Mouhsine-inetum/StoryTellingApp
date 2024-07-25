using System.Net.Http.Headers;
using System.Text.Json;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;
namespace StoryTellingApp.Factory
{
    public class GeneralCLient<T> : IGeneralCLient<T> where T : class
    {
        private IHttpClientFactory _httpClientFactory;
        public GeneralCLient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<T>> Get()
        {
            HttpClient client = _httpClientFactory.CreateClient("task");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Tag/All");
            var data = await response.Content.ReadAsStreamAsync();
            var entity = await JsonSerializer.DeserializeAsync<List<T>>(data,new JsonSerializerOptions()
            {
                 PropertyNameCaseInsensitive = true,
            });
            return entity;
        }
        public async Task<Vm<T>> GetVM()
        {
            HttpClient client = _httpClientFactory.CreateClient("task");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Tag/All");
            var data = await response.Content.ReadAsStreamAsync();
            var entity = await JsonSerializer.DeserializeAsync<Vm<T>>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
            return entity;
        }
    }
}

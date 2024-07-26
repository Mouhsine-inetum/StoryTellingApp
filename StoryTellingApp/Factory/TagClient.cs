using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using StoryTellingApp.Data.Services;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace StoryTellingApp.Factory
{
    public class TagClient : ITagClient
    {

        private IHttpClientFactory _httpClientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;
        public TagClient(IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisition,IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public async Task<TagVm> GetVM()
        {
            HttpClient client = _httpClientFactory.CreateClient("task");
            var scopes = _configuration["UserApiOne:ScopeForAccessToken"].Split(' ');
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync( scopes !,authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme);
            TagVm entity;
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Tag/All");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStreamAsync();
                 entity = await JsonSerializer.DeserializeAsync<TagVm>(data, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
            }

            catch (Exception ex)
            {
                return new TagVm()
                {
                    Tags= new List<Tags>()
                };
            }
            return entity;
        }
    }
}

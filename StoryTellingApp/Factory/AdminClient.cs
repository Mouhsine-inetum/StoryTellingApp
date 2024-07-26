using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StoryTellingApp.Factory
{
    public class AdminClient : IAdminClient
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        public PaginatedItems<UserDisplay> userItems;
        public AdminClient(IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }


        public async Task<PaginatedItems<UserDisplay>> Get()
        {
            HttpClient client = _httpClientFactory.CreateClient("task");
            var scopes = _configuration["AdminApiOne:ScopeForAccessToken"].Split(' ');
            PaginatedItems<UserDisplay> entity;

            try
            {
                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes!, authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "User/{user_id}");

                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStreamAsync();
                entity = await JsonSerializer.DeserializeAsync<PaginatedItems<UserDisplay>>(data, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

            }

            catch (Exception ex)
            {
                return new PaginatedItems<UserDisplay>()
                {
                    Items = new List<UserDisplay>()
                };
            }
            return entity;
        }

        public async Task<IResult> Delete()
        {
            HttpClient client = _httpClientFactory.CreateClient("task");
            var scopes = _configuration["AdminApiOne:ScopeForAccessToken"].Split(' ');
           
            try
            {
                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes!, authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessToken);
                HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + "UseCaseAdmin/{user_id}");
                response.EnsureSuccessStatusCode();
            }

            catch (Exception ex)
            {
               return Results.BadRequest(ex);
            }
            return Results.Ok();
        }

      
    }
}

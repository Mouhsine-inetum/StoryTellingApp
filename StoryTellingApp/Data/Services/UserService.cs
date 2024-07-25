using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using StoryTellingApp.Data.Entity;
using StoryTellingApp.Data.Extensions;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StoryTellingApp.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AccountDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClient;
        public UserService(AccountDbContext dbContext, IHttpContextAccessor httpContext,IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
            _httpClient = httpClientFactory;
        }

    
        public async Task<IResult> Create(Users user)
        {
           HttpClient userClient = _httpClient.CreateClient("task");
           string data = JsonSerializer.Serialize(user);
           StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            
            try
            {
              var response = await userClient.PostAsync("User", content);
              response.EnsureSuccessStatusCode();
            }
            catch(Exception ex) { return Results.BadRequest(ex.Message); }
            return Results.Ok();
            //await _dbContext.Users.AddAsync(user);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetB2cTokenAsync()
        {
            try
            {
                return await _httpContext.HttpContext.GetTokenAsync("access_token");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> GetById(string b2cObjId)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u=>u.b2cObjId==b2cObjId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> GetUserFromSession()
        {
            var user = _httpContext.HttpContext.Session.GetComplexData<Users>("UserSession");
            if (user == null || string.IsNullOrWhiteSpace(user.b2cObjId))
            {
                var idClaim = ((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier);
                string userId= idClaim?.Value;
                user = await GetById(userId);
                this._httpContext.HttpContext.Session.SetComplexData("UserSession", user);
            }
            return user;
        }
    }
}

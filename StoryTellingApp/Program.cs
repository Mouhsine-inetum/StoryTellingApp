using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using StoryTellingApp.Data;
using StoryTellingApp.Data.Entity;
using StoryTellingApp.Data.Services;
using StoryTellingApp.Data.Settings;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory;
using StoryTellingApp.Factory.Interfaces;
using StoryTellingApp.Pages;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
//Initialize variables from config
var authSection = builder.Configuration.GetSection("AzureAdB2c");
var scopes = authSection.GetSection("Scope");
authSection.GetSection("ClientSecret").Value = Environment.GetEnvironmentVariable("SecretAzure", EnvironmentVariableTarget.User);
string[] initialScopes = builder.Configuration.GetValue<string>("UserApiOne:ScopeForAccessToken")?.Split(' ');

//Initialize Service for accessing context from various other services
builder.Services.AddHttpContextAccessor();

//Initialize HttpClient to call the api
builder.Services.AddHttpClient("task",options =>
{
    options.BaseAddress =new Uri(builder.Configuration.GetSection("UserApiOne:ApiBaseAddress").Value);
    var mt = new MediaTypeWithQualityHeaderValue("application/json");
    options.DefaultRequestHeaders.Accept.Add(mt);
});

builder.Services.AddScoped<IGeneralCLient<Tags>, GeneralCLient<Tags>>();
builder.Services.AddScoped<ITagClient, TagClient>();

//session services
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromDays(1);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});



var AuthorityBase = $"{authSection.GetValue<string>("Instance")}{authSection.GetValue<string>("Domain")}/";
var editProfileId = authSection.GetValue<string>("EditProfilePolicyId");
var AuthorityEditProfile = $"{AuthorityBase}{editProfileId}/v2.0";



// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Privacy");
    options.Conventions.AuthorizePage("/Accounts/Logout");
    options.Conventions.AuthorizePage("/Tags/Index");

}).AddMicrosoftIdentityUI();

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSecurity"));
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C")
        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
        .AddInMemoryTokenCaches();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddOpenIdConnect("B2C_1_b2c_editProfile", OpenIdConnect_Settings.GetOpenIdConnectOptions(editProfileId, authSection, AuthorityEditProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();

app.MapRazorPages();
//app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace StoryTellingApp.Data.Settings
{
    public static class OpenIdConnect_Settings
    {
        internal static Action<OpenIdConnectOptions> GetOpenIdConnectOptions(string policy,IConfigurationSection authSection,string AuthorityEditProfile) => options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = AuthorityEditProfile;
            options.ClientId = authSection.GetValue<string>("ClientId");
            options.ClientSecret = Environment.GetEnvironmentVariable("SecretAzure",EnvironmentVariableTarget.User);
            options.SaveTokens = true;
            options.CallbackPath = "/signin-oidc-" + policy;
            options.Scope.Add("https://talestale.onmicrosoft.com/860a243f-8d96-47d1-85b0-7971f7ca99a2/access_as_user");
            options.Scope.Add("https://talestale.onmicrosoft.com/860a243f-8d96-47d1-85b0-7971f7ca99a2/read:item");
            options.ResponseType = "code";
    
            options.Events = new OpenIdConnectEvents()
            {
                OnMessageReceived = context =>
                {
                    if (!string.IsNullOrEmpty(context.ProtocolMessage.Error) && !string.IsNullOrEmpty(context.ProtocolMessage.ErrorDescription))
                    {
                        if (context.ProtocolMessage.Error.Contains("access_denied"))
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/");
                        }
                    }
                    return Task.FromResult(0);
                }
            };
        };
    }
}

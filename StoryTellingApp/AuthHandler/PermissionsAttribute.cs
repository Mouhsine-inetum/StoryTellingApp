using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using StoryTellingApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace StoryTellingApp.AuthHandler
{
    public class PermissionsAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public string Role {  get; set; }
        private IUserService _userService;

        public PermissionsAttribute(string role)
        {
            Role = role;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _userService = context.HttpContext.RequestServices.GetService<IUserService>();
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserFromSession();
                var newClaim = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, user.userRole) });

                context.HttpContext.User.AddIdentity(newClaim);
                if(user.userRole  == Role)
                {
                    return;
                }

                context.Result = new StatusCodeResult(403);
                context.Result = new RedirectResult("/error/403");
                return;
            }

            context.Result = new StatusCodeResult(401);
            context.Result = new RedirectResult("/error/401");

            return;
        }
    }
}

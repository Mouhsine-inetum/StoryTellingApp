using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoryTellingApp.Data.Entity;
using StoryTellingApp.Data.Services;
using StoryTellingApp.Entity;
using StoryTellingApp.Factory.Interfaces;
using System.Security.Claims;

namespace StoryTellingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        public IndexModel(ILogger<IndexModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task OnGet()
        {
            //await InsertAuthUser();
        }

        private async Task InsertAuthUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var b2cObjId = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                var emails = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("emails").Value;
                var user = await _userService.GetById(b2cObjId);
                if (user == null || string.IsNullOrEmpty(user.b2cObjId))
                {
                    var role = "member";
                    var LocalUserId = "Local_"+b2cObjId;
                    await _userService.Create(new Users()
                    {
                        user_id = LocalUserId,
                        b2cObjId = b2cObjId,
                        userRole = role,
                        email = emails
                    });

                }
            }
        }
    }
}

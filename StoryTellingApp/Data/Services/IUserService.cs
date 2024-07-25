using StoryTellingApp.Data.Entity;

namespace StoryTellingApp.Data.Services
{
    public interface IUserService
    {
        Task<IResult> Create(Users user);
        Task<Users> GetById (string b2cObjId);
        Task<string> GetB2cTokenAsync();
        Task<Users> GetUserFromSession();
    }
}

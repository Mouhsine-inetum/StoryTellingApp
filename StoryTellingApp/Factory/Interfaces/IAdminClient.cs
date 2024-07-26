using StoryTellingApp.Entity;

namespace StoryTellingApp.Factory.Interfaces
{
    public interface IAdminClient
    {
        public Task<PaginatedItems<UserDisplay>> Get();
        public Task<IResult> Delete();
    }
}

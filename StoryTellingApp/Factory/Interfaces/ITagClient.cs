using StoryTellingApp.Entity;

namespace StoryTellingApp.Factory.Interfaces
{
    public interface ITagClient
    {
        public Task<TagVm> GetVM();

    }
}

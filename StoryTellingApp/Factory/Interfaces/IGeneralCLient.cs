using StoryTellingApp.Entity;

namespace StoryTellingApp.Factory.Interfaces
{
    public interface IGeneralCLient<T> where T : class
    {
        Task<IList<T>> Get();
        Task<Vm<T>> GetVM();
    }
}
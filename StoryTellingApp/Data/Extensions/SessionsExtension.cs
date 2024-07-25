using System.Text.Json;
using System.Text.Json.Serialization;

namespace StoryTellingApp.Data.Extensions
{
    public static class SessionsExtension
    {
        public static T GetComplexData<T> (this ISession session,string key) where T : class
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(data);
        }

        public static void SetComplexData (this ISession session, string key, object value) 
        {
            session.SetString(key,JsonSerializer.Serialize(value));
        }
    }
}

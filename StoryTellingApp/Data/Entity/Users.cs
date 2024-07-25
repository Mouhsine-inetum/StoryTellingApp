using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoryTellingApp.Data.Entity
{
    public class Users
    {
        [JsonIgnore]
        [Key]
        public string user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string? avatar { get; set; }
        public string b2cObjId { get; set; }
        public string userRole { get; set; }

    }
}

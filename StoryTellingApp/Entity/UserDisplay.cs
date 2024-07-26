using Microsoft.Graph;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace StoryTellingApp.Entity
{
    public class UserDisplay 
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string? avatar { get; set; }//new 
       
    }
}

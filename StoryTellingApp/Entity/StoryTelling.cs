namespace StoryTellingApp.Entity
{
    public class StoryTelling
    {
        public int IdStoryTelling { get; set; }
        public string? url { get; set; }//new
        public string NameStory { get; set; }
        public string user_id { get; set; }
        public string Sypnopsis { get; set; }
        public int IdZone { get; set; }//new
        public bool Finished { get; set; }
        public int numberRef { get; set; }
        public int? idTag { get; set; }
        public double? rating { get; set; }//nouveau
        public DateTime DateCreation { get; set; }
        public int Signal { get; set; }
    }
}

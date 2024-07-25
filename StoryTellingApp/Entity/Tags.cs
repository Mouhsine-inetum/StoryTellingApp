namespace StoryTellingApp.Entity
{
    public class Tags
    {
        public int IdTag { get; set; }
        public string NameTag { get; set; }
        public double numberRef { get; set; }
    }

    public class TagVm
    {
        public IList<Tags> Tags { get; set; }
    }
}

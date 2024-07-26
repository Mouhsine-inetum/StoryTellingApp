namespace StoryTellingApp.Entity
{
    public class PaginatedItems<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
    }
}

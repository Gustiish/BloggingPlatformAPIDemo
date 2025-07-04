namespace BloggingPlatformAPIDemo
{
    public class Blog
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }

        public Blog(int id, string title, string content, string[] tags)
        {
            Id = id;
            Title = title;
            Content = content;
            Tags = tags;

        }




    }
}

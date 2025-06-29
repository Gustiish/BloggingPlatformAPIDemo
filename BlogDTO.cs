namespace BloggingPlatformAPIDemo
{
    public class BlogDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public BlogDTO(string title, string content)
        {
            Title = title;
            Content = content;


        }

        public BlogDTO()
        {
            
        }
    }
}

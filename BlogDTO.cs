using System.Text.Json.Serialization;

namespace BloggingPlatformAPIDemo
{
    public class BlogDTO
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        public BlogDTO(string title, string content, string[] tags)
        {
            Title = title;
            Content = content;
            Tags = tags;


        }

        public BlogDTO()
        {

        }
    }
}

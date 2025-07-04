using System.Text.Json.Serialization;

namespace BloggingPlatformAPIDemo
{
    public class PatchBlogDTO
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("tags")]
        public string[]? Tags { get; set; }
    }
}

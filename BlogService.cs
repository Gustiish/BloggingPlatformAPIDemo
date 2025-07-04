using System.Text.Json;

namespace BloggingPlatformAPIDemo
{
    public static class BlogService
    {
        public static List<Blog> _blogList = new List<Blog>();
        private static int nextId = 1;
        private static readonly string folder = @"C:\Users\Isak Bäckström\Source\Repos\BloggingPlatformAPIDemo";
        private static readonly string filename = "data.json";
        private static readonly string JsonPath = Path.Combine(folder, filename);

        public static Blog CreatePost(string title, string content, string[] tags)
        {
            Blog blog = new Blog(nextId++, title, content, tags);
            _blogList.Add(blog);
            SaveToJson();
            return blog;
        }

        public static Blog FindPostById(int id)
        {

            return _blogList.FirstOrDefault(b => b.Id == id);
        }

        public static void DeletePost(Blog blog)
        {
            _blogList.Remove(blog);
            UpdateId();
            SaveToJson();
        }

        public static List<Blog> GetAll()
        {
            return _blogList;
        }

        public static void UpdatePost(Blog blog)
        {
            int index = _blogList.IndexOf(blog);
            if (index != -1)
            {
                _blogList[index] = blog;
            }
            SaveToJson();
        }

        public static List<Blog> GetByTag(string tag)
        {
            var posts = _blogList.Where(blog => blog.Tags.Contains(tag)).ToList();
            return posts;
        }

        public static void LoadData()
        {
            string Json = File.ReadAllText(JsonPath);
            if (!string.IsNullOrEmpty(Json))
            {
                _blogList = JsonSerializer.Deserialize<List<Blog>>(Json) ?? Enumerable.Empty<Blog>().ToList();
                UpdateId();
            }

        }

        public static void LoadNextId()
        {
            if (_blogList.Count == 0)
            {
                nextId = 1;
            }
            else
            {
                nextId = _blogList.Max(b => b.Id) + 1;
            }
        }

        public static void UpdateId()
        {
            foreach (Blog blog in _blogList)
            {
                blog.Id = _blogList.IndexOf(blog) + 1;
            }
        }

        public static void SaveToJson()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize<List<Blog>>(_blogList, options);

            File.WriteAllText(JsonPath, json);

        }
    }
}

namespace BloggingPlatformAPIDemo
{
    public static class BlogService
    {
        public static List<Blog> _blogList = new List<Blog>();
        private static int nextId = 1;

        public static Blog CreatePost(string title, string content, string[] tags)
        {
            Blog blog = new Blog(nextId++, title, content, tags);
            _blogList.Add(blog);
            return blog;
        }

        public static Blog FindPostById(int id)
        {

            return _blogList.FirstOrDefault(b => b.Id == id);
        }

        public static void DeletePost(Blog blog)
        {
            _blogList.Remove(blog);
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
        }

        public static List<Blog> GetByTag(string tag)
        {
            var posts = _blogList.Where(blog => blog.Tags.Contains(tag)).ToList();
            return posts;
        }

    }
}

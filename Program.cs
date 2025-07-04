using StackExchange.Redis;

namespace BloggingPlatformAPIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            var db = redis.GetDatabase();

            app.MapGet("/blog", async (HttpRequest req) =>
            {
                var idString = req.Query["id"];
                if (int.TryParse(idString, out var id))
                {
                    Blog blog = BlogService._blogList.Find(b => b.Id == id);
                    if (blog == null)
                    {
                        return Results.NotFound($"No blog with Id {id}");
                    }
                    return Results.Ok(blog);
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapPost("/blog", async (HttpRequest req) =>
            {
                try
                {
                    BlogDTO newBlog = await req.ReadFromJsonAsync<BlogDTO>();
                    var blog = BlogService.CreatePost(newBlog.Title, newBlog.Content, newBlog.Tags);
                    return Results.Created($"/blog/{blog.Id}", blog);
                }
                catch
                {
                    return Results.Problem();
                }


            });

            app.MapDelete("/blog", async (HttpRequest req) =>
            {
                var idString = req.Query["id"];
                if (int.TryParse(idString, out var id))
                {
                    var blog = BlogService.FindPostById(id);
                    if (blog == null)
                    {
                        return Results.NotFound("The blogpost was not found");
                    }
                    else
                    {
                        BlogService.DeletePost(blog);
                        return Results.Ok($"The blog {blog.Id} was deleted");
                    }


                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapPut("/blog", async (HttpRequest req) =>
            {
                var idString = req.Query["id"];
                if (int.TryParse(idString, out var id))
                {
                    var existingBlog = BlogService.FindPostById(id);
                    if (existingBlog == null)
                    {
                        return Results.BadRequest("The id is invalid");
                    }
                    BlogDTO updatedBlog = await req.ReadFromJsonAsync<BlogDTO>();
                    if (updatedBlog == null)
                    {
                        return Results.BadRequest("Updated blog is null");
                    }

                    existingBlog.Title = updatedBlog.Title;
                    existingBlog.Content = updatedBlog.Content;
                    BlogService.UpdatePost(existingBlog);
                    return Results.Ok(existingBlog);
                }

                else
                {
                    return Results.NotFound("Invalid id");
                }

            });

            app.MapPatch("/blog", async (HttpRequest req) =>
            {
                var idString = req.Query["id"];
                if (int.TryParse(idString, out int id))
                {
                    PatchBlogDTO patchBlogDTO = await req.ReadFromJsonAsync<PatchBlogDTO>();
                    var existingBlog = BlogService.FindPostById(id);
                    if (existingBlog == null)
                    {
                        return Results.BadRequest("The blog was not found");
                    }

                    if (!string.IsNullOrWhiteSpace(patchBlogDTO.Title))
                        existingBlog.Title = patchBlogDTO.Title;
                    if (!string.IsNullOrWhiteSpace(patchBlogDTO.Content))
                        existingBlog.Content = patchBlogDTO.Content;

                    BlogService.UpdatePost(existingBlog);
                    return Results.Ok(existingBlog);


                }
                else
                {
                    return Results.NotFound("Blog not found");
                }
            });

            app.MapGet("/posts", async (HttpRequest req) =>
            {
                var posts = BlogService.GetAll();
                if (posts == null)
                    return Results.NotFound();

                try
                {
                    return Results.Ok(posts);
                }
                catch
                {
                    return Results.BadRequest();
                }
            });

            app.MapGet("/post/tag", async (HttpRequest req) =>
            {
                string tag = req.Query["tag"];
                if (tag == null)
                {
                    return Results.BadRequest();
                }

                var posts = BlogService.GetByTag(tag);
                if (posts == null)
                    return Results.NotFound();

                return Results.Ok(posts);

            });

            //Redis test
            app.MapGet("/redis", async () =>
            {
                string testKey = "testKey";
                string testValue = "Hello from Redis";

                await db.StringSetAsync(testKey, testValue);

                string? value = await db.StringGetAsync(testKey);

                return Results.Ok(value);
            });



            app.Run();


        }
    }
}



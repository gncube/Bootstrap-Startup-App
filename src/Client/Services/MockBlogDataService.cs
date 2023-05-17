using GSN.Domain;

namespace Client.Services;

public class MockBlogDataService : IBlogDataService
{
    private static List<Blog> _blogs = default!;

    public static List<Blog> Blogs
    {
        get
        {
            _blogs ??= InitializeMockBlogs();
            return _blogs;
        }
    }

    private static List<Blog> InitializeMockBlogs()
    {
        var b1 = new Blog
        {
            Id = "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b",
            BlogName = "First Blog",
            Published = true,
            CreatedOnDate = DateTime.UtcNow
        };

        var b2 = new Blog
        {
            Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
            BlogName = "Second Blog Test",
            Published = true,
            CreatedOnDate = DateTime.UtcNow
        };

        return new List<Blog>() { b1, b2 };
    }

    public Task<Blog> CreateBlogAsync(Blog blog)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBlogAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Blog>> GetAllBlogsAsync(bool refreshRequired = false)
    {
        throw new NotImplementedException();
    }

    public async Task<Blog> GetBlogByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Blog>> GetBlogsAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateBlogAsync(Blog blog)
    {
        throw new NotImplementedException();
    }
}

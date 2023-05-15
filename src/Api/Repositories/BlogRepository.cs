using GSN.Application;
using GSN.Domain;

namespace Api.Repositories;
internal class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _context;

    public BlogRepository(AppDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreatedAsync();
    }
    public IEnumerable<Blog> GetAllBlogs()
    {
        return _context.Blogs.ToList();
    }

    public Blog GetBlogById(string id)
    {
        throw new NotImplementedException();
    }

    public Blog AddBlog(Blog blog)
    {
        throw new NotImplementedException();
    }

    public Blog UpdateBlog(Blog blog)
    {
        throw new NotImplementedException();
    }

    public void DeleteBlog(string id)
    {
        throw new NotImplementedException();
    }
}

using GSN.Domain;

namespace Api.Data;

public class BlogRepository : IBlogRepository
{
    private readonly BlogifierDbContext _context;

    public BlogRepository(BlogifierDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreatedAsync();
    }
    public Blog AddBlog(Blog blog)
    {
        var addedBlog = _context.Blogs.Add(blog);
        _context.SaveChanges();
        return addedBlog.Entity;
    }

    public void DeleteBlog(string id)
    {
        var foundBlog = _context.Blogs.FirstOrDefault(b => b.Id == id);
        if (foundBlog == null)
            return;

        _context.Blogs.Remove(foundBlog);
        _context.SaveChanges();
    }

    public IEnumerable<Blog> GetAllBlogs()
    {
        return _context.Blogs.ToList();
    }

    public Blog GetBlogById(string id)
    {
        return _context.Blogs.FirstOrDefault(b => b.Id == id);
    }

    public Blog UpdateBlog(Blog blog)
    {
        var foundBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);
        if (foundBlog != null)
        {
            foundBlog.BlogName = blog.BlogName;
            foundBlog.Published = blog.Published;

            _context.SaveChanges();
            return foundBlog;
        }
        return null;
    }
}

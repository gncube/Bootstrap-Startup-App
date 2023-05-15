using GSN.Domain;

namespace GSN.Application;
public interface IBlogRepository
{
    IEnumerable<Blog> GetAllBlogs();
    Blog GetBlogById(string id);
    Blog AddBlog(Blog blog);
    Blog UpdateBlog(Blog blog);
    void DeleteBlog(string id);
}

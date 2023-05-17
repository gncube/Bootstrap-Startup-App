using GSN.Domain;

namespace Api.Data;

public interface IBlogRepository
{
    IEnumerable<Blog> GetAllBlogs();
    Blog GetBlogById(string id);
    Blog AddBlog(Blog blog);
    Blog UpdateBlog(Blog blog);
    void DeleteBlog(string id);
}

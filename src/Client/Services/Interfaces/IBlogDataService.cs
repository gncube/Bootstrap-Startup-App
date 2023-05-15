using GSN.Domain;

namespace Client.Services;

public interface IBlogDataService
{
    Task<IEnumerable<Blog>> GetAllBlogsAsync(bool refreshRequired = false);
    Task<IEnumerable<Blog>> GetBlogsAsync();
    Task<Blog> GetBlogByIdAsync(string id);
    Task<Blog> CreateBlogAsync(Blog blog);
    Task UpdateBlogAsync(Blog blog);
    Task DeleteBlogAsync(string id);
}

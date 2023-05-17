using Blazored.LocalStorage;
using Client.Helper;
using GSN.Domain;
using System.Text;
using System.Text.Json;

namespace Client.Services;

public class BlogDataService : IBlogDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public BlogDataService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    public async Task<Blog> CreateBlogAsync(Blog blog)
    {
        var blogJson = new StringContent(JsonSerializer.Serialize(blog), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/blogs", blogJson);

        if (response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<Blog>(await response.Content.ReadAsStreamAsync());
        }

        return null;
    }

    public async Task DeleteBlogAsync(string id)
    {
        await _httpClient.DeleteAsync($"api/blogs/{id}");
    }

    public async Task<IEnumerable<Blog>> GetAllBlogsAsync(bool refreshRequired = false)
    {
        if (!refreshRequired)
        {
            bool blogExpirationExists = await
                _localStorageService.ContainKeyAsync(LocalStorageConstants.BlogListExpirationKey);
            if (blogExpirationExists)
            {
                DateTime blogListExpiration = await _localStorageService.GetItemAsync<DateTime>(LocalStorageConstants.BlogListExpirationKey);
                if (blogListExpiration > DateTime.Now) // get from local storage
                {
                    if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.BlogsListKey))
                    {
                        return await _localStorageService.GetItemAsync<IEnumerable<Blog>>(LocalStorageConstants.BlogsListKey);
                    }
                }
            }
        }

        //otherwise refresh the list locally from the API and set expiration to 1 minute in future

        using var responseStream = await _httpClient.GetStreamAsync($"api/blogs");
        using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

        var blogsArray = jsonDoc.RootElement.GetProperty("Value");

        var blogs = JsonSerializer.Deserialize<IEnumerable<Blog>>(blogsArray.GetRawText(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        await _localStorageService.SetItemAsync(LocalStorageConstants.BlogsListKey, blogs);
        await _localStorageService.SetItemAsync(LocalStorageConstants.BlogListExpirationKey, DateTime.Now.AddMinutes(1));

        return blogs;
    }

    public async Task<Blog> GetBlogByIdAsync(string id)
    {
        using var responseStream = await _httpClient.GetStreamAsync($"api/blogs/{id}");
        using var jsonDoc = await JsonDocument.ParseAsync(responseStream);
        var blogJson = jsonDoc.RootElement.GetProperty("Value");

        var blog = JsonSerializer.Deserialize<Blog>(blogJson.GetRawText(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return blog;
    }

    public Task<IEnumerable<Blog>> GetBlogsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateBlogAsync(Blog blog)
    {
        var blogJson = new StringContent(JsonSerializer.Serialize(blog), Encoding.UTF8, "application/json");

        await _httpClient.PutAsync($"api/blogs/{blog.Id}", blogJson);
    }
}

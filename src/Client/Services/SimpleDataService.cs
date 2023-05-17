using Newtonsoft.Json;
using GSN.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

public class SimpleDataService : IBlogDataService
{
    private readonly HttpClient _httpClient;

    public SimpleDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        return _httpClient.GetFromJsonAsync<IEnumerable<Blog>>("api/blogs");
    }

    public Task<Blog> GetBlogByIdAsync(string id)
    {
        return _httpClient.GetFromJsonAsync<Blog>($"api/blogs/{id}");
    }

    public async Task<IEnumerable<Blog>> GetBlogsAsync()
    {
        var response = await _httpClient.GetAsync("api/blogs");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON object
        using var jsonDoc = JsonDocument.Parse(jsonString);

        // Extract the 'Value' property containing the array of Blog objects and deserialize it
        var blogsArray = jsonDoc.RootElement.GetProperty("Value");

        return JsonConvert.DeserializeObject<IEnumerable<Blog>>(blogsArray.GetRawText());
    }

    public Task UpdateBlogAsync(Blog blog)
    {
        throw new NotImplementedException();
    }
}

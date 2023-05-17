using Api.Data;
using Api.Dtos;
using GSN.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api;

public class BlogApi
{
    private readonly ILogger _logger;
    private readonly IBlogRepository _repo;

    public BlogApi(ILoggerFactory loggerFactory, IBlogRepository repo)
    {
        _logger = loggerFactory.CreateLogger<BlogApi>();
        _repo = repo;
    }

    [Function(nameof(CreateBlog))]
    public async Task<IActionResult> CreateBlog(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "blogs")] HttpRequestData req)
    {
        _logger.LogInformation($"{nameof(CreateBlog)} ---> Processed the blogs");

        var requestData = await new StreamReader(req.Body).ReadToEndAsync();
        var blogCreateDto = JsonConvert.DeserializeObject<BlogCreateDto>(requestData);

        var blog = new Blog
        {
            BlogName = blogCreateDto.BlogName,
        };

        _repo.AddBlog(blog);

        return new OkObjectResult(blog);
    }

    [Function(nameof(GetBlogs))]
    public async Task<IActionResult> GetBlogs(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "blogs")] HttpRequestData req)
    {
        _logger.LogInformation($"{nameof(GetBlogs)} ---> Getting all the blogs");

        //await _context.Database.EnsureCreatedAsync();

        var blogs = _repo.GetAllBlogs();

        return new OkObjectResult(blogs);
    }

    [Function(nameof(GetBlogById))]
    public async Task<IActionResult> GetBlogById(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "blogs/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation($"{nameof(GetBlogById)} ---> Getting the blogToDelete with ID: {id}");

        //await _context.Database.EnsureCreatedAsync();

        var blog = _repo.GetBlogById(id);
        if (blog == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(blog);
    }

    [Function(nameof(PutBlog))]
    public async Task<IActionResult> PutBlog(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "blogs/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation($"{nameof(PutBlog)} ---> Updating the blogToDelete with ID: {id}");

        //await _context.Database.EnsureCreatedAsync();

        var blog = _repo.GetBlogById(id);
        if (blog == null)
        {
            _logger.LogWarning($"---> Blog could not be found!");
            return new NotFoundResult();
        }

        var requestData = await new StreamReader(req.Body).ReadToEndAsync();
        var blogUpdateDto = JsonConvert.DeserializeObject<BlogUpdateDto>(requestData);

        blog.BlogName = blogUpdateDto.BlogName;
        blog.Published = blogUpdateDto.Published;

        _repo.UpdateBlog(blog);

        return new OkObjectResult(blog);
    }

    [Function(nameof(DeleteBlog))]
    public async Task<IActionResult> DeleteBlog(
    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "blogs/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation($"{nameof(DeleteBlog)} ---> Deleting the blog with ID: {id}");

        //await _context.Database.EnsureCreatedAsync();

        var blogToDelete = _repo.GetBlogById(id);
        if (blogToDelete == null)
        {
            _logger.LogWarning($"---> Blog could not be found!");
            return new NotFoundResult();
        }

        _repo.DeleteBlog(blogToDelete.Id);

        return new OkResult();
    }
}

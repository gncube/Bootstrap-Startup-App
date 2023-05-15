using GSN.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

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

    [Function("BlogApi")]
    public async Task<IActionResult> BlogsGet(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "blogs")] HttpRequestData req)
    {
        _logger.LogInformation($"{nameof(BlogsGet)} function processed a request.");

        var blogs = _repo.GetAllBlogs();

        return new OkObjectResult(blogs);
    }
}

using Client.Services;
using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class Counter
{
    [Inject]
    public IBlogDataService? BlogService { get; set; }

    public IEnumerable<Blog>? Blogs { get; set; } = default!;

    private Blog? _selectedBlog;

    protected override async Task OnInitializedAsync()
    {
        Blogs = await BlogService.GetAllBlogsAsync(true);
    }
}

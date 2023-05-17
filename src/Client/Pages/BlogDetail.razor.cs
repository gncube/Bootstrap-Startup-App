using Client.Services;
using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class BlogDetail
{
    [Inject]
    public IBlogDataService? BlogDataService { get; set; }

    [Parameter]
    public string Id { get; set; }

    public Blog? Blog { get; set; } = new Blog();

    protected override async Task OnInitializedAsync()
    {
        Blog = await BlogDataService.GetBlogByIdAsync(Id);
    }
}

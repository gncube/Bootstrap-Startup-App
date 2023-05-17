using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class BlogCard
{
    [Parameter]
    public Blog Blog { get; set; } = default;

    [Parameter]
    public EventCallback<Blog> BlogQuickViewClicked { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        if (string.IsNullOrEmpty(Blog.BlogName))
            throw new Exception("Blog name can't be empty!");
    }

    public void NavigateToDetails(Blog selectedBlog)
    {
        NavigationManager.NavigateTo($"/blogs/{selectedBlog.Id}");
    }
}

using Client.Services;

namespace Client.Components.Widgets;

public partial class BlogCountWidget
{
    public int BlogCounter { get; set; }

    protected override void OnInitialized()
    {
        BlogCounter = MockBlogDataService.Blogs.Count;
    }
}

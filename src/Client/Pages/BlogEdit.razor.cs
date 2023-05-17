using Client.Services;
using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class BlogEdit
{
    [Inject]
    public IBlogDataService? BlogService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public string? Id { get; set; }

    public Blog Blog { get; set; } = new Blog();

    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved;

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            Blog = new Blog { CreatedOnDate = DateTime.UtcNow, Published = true };
        }
        else
        {
            Blog = await BlogService.GetBlogByIdAsync(Id);
        }
    }

    protected async Task HandleValidSubmit()
    {
        Saved = false;

        if (string.IsNullOrEmpty(Id)) // new 
        {
            var addedBlog = await BlogService.CreateBlogAsync(Blog);
            if (addedBlog != null)
            {
                StatusClass = "alert-success";
                Message = "New blog added successfully!";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding new blog. Please try again!";
                Saved = false;
            }
        }
        else
        {
            await BlogService.UpdateBlogAsync(Blog);
            StatusClass = "alert-success";
            Message = "Blog updated successfully!";
            Saved = true;
        }
    }

    protected async Task HandleInvalidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please fixe and try again!";
    }

    protected async Task DeleteBlog()
    {
        await BlogService.DeleteBlogAsync(Blog.Id);

        StatusClass = "alert-success";
        Message = "Deleted Successfully";

        Saved = true;
    }

    protected void NavigateToOverview()
    {
        NavigationManager.NavigateTo("/blogoverview");
    }
}

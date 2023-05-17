
using GSN.Domain;
using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class QuickViewPopup
{
    private Blog? _blog;

    [Parameter]
    public Blog Blog { get; set; }

    protected override void OnParametersSet()
    {
        _blog = Blog;
    }

    public void Close()
    {
        _blog = null;
    }
}

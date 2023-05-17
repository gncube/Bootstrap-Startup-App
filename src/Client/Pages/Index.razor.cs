using Client.Components.Widgets;

namespace Client.Pages
{
    public partial class Index
    {
        public List<Type> Widgets { get; set; } = new List<Type>()
        {
            typeof(BlogCountWidget), typeof(InboxWidget)
        };

        public int MyProperty { get; set; }
    }
}

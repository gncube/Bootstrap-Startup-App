namespace GSN.Domain;

public class Post
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Content { get; set; }
    public string BlogId { get; set; }
    public Blog Blog { get; set; }
    public Name AuthorName { get; set; }
}
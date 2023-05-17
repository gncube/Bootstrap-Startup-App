namespace Api.Dtos;

public class BlogReadDto
{
    public string Id { get; set; }
    public string BlogName { get; set; }
    public bool Published { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
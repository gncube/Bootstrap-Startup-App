using System.ComponentModel.DataAnnotations;

namespace GSN.Domain;
public class Blog
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string BlogName { get; set; }
    public bool Published { get; set; }
    public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Post> Posts { get; set; }
}

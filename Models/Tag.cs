using System;

namespace BloggingPlatform.Models;

public class Tag
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public ICollection<BlogTag> Tags { get; set; } = null!;
}

using System;

namespace BloggingPlatform.Models;

public class Tag
{
    public required string TagId { get; set; }
    public ICollection<BlogTag> Tags { get; set; } = new List<BlogTag>();
}

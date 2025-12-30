using System;

namespace BloggingPlatform.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public ICollection<Blog>? Blogs { get; set; } = new List<Blog>();
}

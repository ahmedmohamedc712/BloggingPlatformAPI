using System;

namespace BloggingPlatform.Models;

public class BlogTag
{
    public int TagId { get; set; }
    public int BlogId { get; set; }
    public Tag Tag {get;set;} = null!;
    public Blog Blog {get;set;} = null!;
}

using System;
using System.Text.Json.Serialization;

namespace BloggingPlatform.Models;

public class BlogTag
{
    public string TagId { get; set; } = null!;
    public int BlogId { get; set; }

    [JsonIgnore]
    public Tag Tag {get;set;} = null!;
    
    [JsonIgnore]
    public Blog Blog {get;set;} = null!;
}

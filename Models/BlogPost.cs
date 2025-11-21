namespace BlazorBlog.Models;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public string AuthorAvatar { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public int Views { get; set; }
    public int Likes { get; set; }
    public string Summary => Content.Length > 100 ? Content.Substring(0, 100) + "..." : Content;
}

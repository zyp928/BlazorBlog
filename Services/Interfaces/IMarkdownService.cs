using Markdig;

namespace BlazorBlog.Services.Interfaces;

public interface IMarkdownService
{
    string ToHtml(string markdown);
}

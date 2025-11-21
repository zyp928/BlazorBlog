using Markdig;
using BlazorBlog.Services.Interfaces;

namespace BlazorBlog.Services;

public class MarkdownService : IMarkdownService
{
    private readonly MarkdownPipeline _pipeline;

    public MarkdownService()
    {
        // 配置 Markdown 管道，启用高级功能
        _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()  // 启用表格、任务列表、脚注等
            .UseEmojiAndSmiley()      // 启用 Emoji
            .UseSoftlineBreakAsHardlineBreak()  // 单换行符转换为 <br>
            .Build();
    }

    public string ToHtml(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return string.Empty;

        return Markdown.ToHtml(markdown, _pipeline);
    }
}

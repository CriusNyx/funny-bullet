using Markdig.Syntax;

public static class MarkdownExtensions
{
  public static string BlockText(this MarkdownObject block, string source)
  {
    return source.Substring(block.Span.Start, block.Span.Length);
  }
}

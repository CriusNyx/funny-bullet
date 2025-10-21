using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Parsers;
using Markdig.Syntax;

public static class Markdown
{
  static Thunk<MarkdownPipeline> FrontMatterPipeline = new Thunk<MarkdownPipeline>(() =>
  {
    var builder = new MarkdownPipelineBuilder();
    builder.Use(new YamlFrontMatterExtension());

    return builder.Build();
  });

  public static MarkdownDocument ParseWithFrontmatter(string source)
  {
    return MarkdownParser.Parse(source, FrontMatterPipeline.Value);
  }
}

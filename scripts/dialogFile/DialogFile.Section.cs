using System.Collections.Generic;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using MDCodeBlock = Markdig.Syntax.CodeBlock;

public partial class DialogFile
{
  /// <summary>
  /// A section of a Dialog file.
  /// </summary>
  public abstract class Block
  {
    public static Block? Parse(MarkdownObject blockHead, Queue<MarkdownObject> queue, string source)
    {
      if (blockHead is YamlFrontMatterBlock frontmatterBlock)
      {
        return DialogMeta.Parse(frontmatterBlock.Lines.ToString());
      }
      else if (blockHead is HeadingBlock headingBlock)
      {
        return ActorBlock.Parse(headingBlock, queue, source);
      }
      else if (blockHead is MDCodeBlock codeBlock)
      {
        return CodeBlock.Parse(codeBlock);
      }
      else
      {
        return null;
      }
    }
  }
}

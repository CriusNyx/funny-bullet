using System.Collections.Generic;
using Markdig.Syntax;

public partial class DialogFile
{
  /// <summary>
  /// Dialog file actor
  /// </summary>
  public class ActorBlock : Block
  {
    public string actorName = "";
    public string? label;
    public ActorBlockContent[] content = [];

    public ActorBlock() { }

    public ActorBlock(string actorName, string? label, ActorBlockContent[] content)
    {
      this.actorName = actorName;
      this.label = label;
      this.content = content;
    }

    public static ActorBlock Parse(HeadingBlock heading, Queue<MarkdownObject> queue, string source)
    {
      var headingText = heading.BlockText(source);
      var trimmedHeading = headingText.TrimStart('#').Trim();
      var (actorName, label) = trimmedHeading.Split(" ").Take2Safe();
      List<ActorBlockContent> contentList = new List<ActorBlockContent>();
      while (
        queue.TryPeek(out var next) && ActorBlockContent.TryParse(next, source, out var content)
      )
      {
        queue.Dequeue();
        contentList.Add(content);
      }
      return new ActorBlock(actorName, label, contentList.ToArray());
    }
  }

  /// <summary>
  /// Content of a dialog file actor block.
  /// </summary>
  public abstract class ActorBlockContent
  {
    public static bool TryParse(
      MarkdownObject markdownObject,
      string source,
      out ActorBlockContent content
    )
    {
      if (markdownObject is ParagraphBlock paragraphBlock)
      {
        content = ActorMessage.Parse(paragraphBlock, source);
        return true;
      }
      else if (markdownObject is ListBlock listBlock)
      {
        content = ActorSelector.Parse(listBlock, source);
        return true;
      }
      content = null!;
      return false;
    }
  }
}

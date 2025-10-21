using System;
using System.Linq;
using Markdig.Syntax;

public partial class DialogFile
{
  /// <summary>
  /// Defines a selector dialog where the player can select an option.
  /// </summary>
  public class ActorSelector : ActorBlockContent
  {
    public string message;
    public string[] options;

    public ActorSelector(string message, string[] options)
    {
      this.message = message;
      this.options = options;
    }

    public static ActorSelector Parse(ListBlock block, string source)
    {
      if (block.First() is ListItemBlock listItemBlock)
      {
        var (titleP, optionBlock) = TryGetChildren(listItemBlock);
        var title = titleP.BlockText(source);
        var options =
          optionBlock
            ?.Select(x => x.As<ListItemBlock>()?.Transform(y => TryGetChildren(y).paragraph))
            .WhereDefined()
            ?.Select(x => x.BlockText(source))
            .ToArray() ?? [];
        return new ActorSelector(title, options);
      }
      throw new InvalidOperationException();
    }

    private static (ParagraphBlock paragraph, ListBlock? childBlock) TryGetChildren(
      ListItemBlock listItemBlock
    )
    {
      var (title, children) = listItemBlock.Take2Safe();
      return (title as ParagraphBlock, children as ListBlock)!;
    }
  }
}

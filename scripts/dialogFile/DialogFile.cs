using System;
using System.Collections.Generic;
using System.Linq;
using Markdig.Syntax;

public partial class DialogFile : DebugPrint
{
  public DialogMeta? Meta => blocks?.FirstOrDefault(x => x is DialogMeta) as DialogMeta;
  public Block[]? blocks = [];

  private static Block[] ParseSections(MarkdownDocument md, string source)
  {
    var queue = new Queue<MarkdownObject>(md.ToArray());
    List<Block> sections = new List<Block>();
    while (queue.TryDequeue(out var nextBlock))
    {
      if (Block.Parse(nextBlock, queue, source) is Block section)
      {
        sections.Add(section);
      }
    }
    return sections.ToArray();
  }

  public int GetBlockIndexWithLabel(string label)
  {
    return this.blocks?.IndexOf(x => x is ActorBlock actorBlock && actorBlock.label == label) ?? -1;
  }

  public static DialogFile Parse(string source)
  {
    var sections = ParseSections(Markdown.ParseWithFrontmatter(source), source);
    return new DialogFile { blocks = sections };
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(blocks).With(blocks)!];
  }

  public class Player { }
}

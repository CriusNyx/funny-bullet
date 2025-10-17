using System.Collections.Generic;
using System.Linq;
using Markdig;
using Markdig.Syntax;

public class DialogFile
{
  public DialogMeta? meta;
  public DialogFileSection[]? sections;

  private static DialogFileSection[] ParseSections(string source)
  {
    var md = Markdown.Parse(source).AsEnumerable();
    var queue = new Queue<MarkdownObject>(md.ToArray());
    List<DialogFileSection> sections = new List<DialogFileSection>();
    while (queue.TryDequeue(out var nextBlock))
    {
      if (DialogFileSection.Parse(nextBlock, queue, source) is DialogFileSection section)
      {
        sections.Add(section);
      }
    }
    return sections.ToArray();
  }

  public static DialogFile Parse(string file)
  {
    var docWithMeta = DocWithMeta.ParseString(file);
    var meta = DialogMeta.Parse(file);
    var sections = ParseSections(docWithMeta.Text ?? "");
    return new DialogFile { meta = meta, sections = sections };
  }
}

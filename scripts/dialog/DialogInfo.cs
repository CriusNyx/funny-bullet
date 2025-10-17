using System.Collections.Generic;
using System.Linq;
using Godot;
using Markdig;
using Markdig.Syntax;
using YamlDotNet.Serialization;

public class DialogInfoParameters : DebugPrint
{
  [YamlMember]
  public Dictionary<string, DialogActorInfo>? actors;

  [YamlMember]
  public Dictionary<string, string>? vars;

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(actors).With(actors)!, nameof(vars).With(vars)!];
  }
}

public class DialogInfo : DebugPrint
{
  static IDeserializer deserializer = new DeserializerBuilder().Build();

  public DialogInfoParameters parameters;
  public DialogBoxInfo[] dialogBoxes;

  public DialogInfo(DialogInfoParameters parameters, DialogBoxInfo[] dialogBoxes)
  {
    this.parameters = parameters;
    this.dialogBoxes = dialogBoxes;
  }

  public static DialogInfo Parse(string value)
  {
    var withMeta = DocWithMeta.ParseString(value);
    var actors = ParseMeta(withMeta.Meta ?? "");
    var dialogBoxes = ParseBody(withMeta.Text ?? "");
    return new DialogInfo(actors, dialogBoxes);
  }

  private static DialogInfoParameters ParseMeta(string meta)
  {
    return deserializer.Deserialize<DialogInfoParameters>(meta);
  }

  private static DialogBoxInfo[] ParseBody(string body)
  {
    string? actor = null;
    var blocks = new List<DialogContent>();
    var dialogs = new List<DialogBoxInfo>();
    var markdown = Markdown.Parse(body);

    void FinalizeBlock()
    {
      if (actor != null)
      {
        dialogs.Add(new DialogBoxInfo(actor!, blocks.ToArray()));
        blocks.Clear();
        actor = null;
      }
    }

    foreach (var block in markdown)
    {
      if (block is HeadingBlock)
      {
        FinalizeBlock();
        actor = block.BlockText(body).Replace("#", "").Trim();
      }
      else if (block is ParagraphBlock paragraph)
      {
        blocks.Add(MessageContainer.Auto(block.BlockText(body)));
      }
      else if (block is ListBlock listBlock)
      {
        blocks.Add(ParseDialogSelector(listBlock, body));
      }
      else
      {
        Debug.WarnUnexpectedType(typeof(Block), block);
      }
    }
    FinalizeBlock();

    return dialogs.ToArray();
  }

  private static DialogSelector ParseDialogSelector(ListBlock listBlock, string source)
  {
    var (title, children) = Decompose(listBlock.First().As<ListItemBlock>().NotNull(), source);
    var options =
      children?.Select(x => Decompose(x.As<ListItemBlock>().NotNull(), source).text).ToArray()
      ?? [];
    return new DialogSelector(title, options);
  }

  private static (string text, ListBlock? children) Decompose(
    ListItemBlock listBlock,
    string source
  )
  {
    var (title, children) = listBlock.Take2Safe();
    var message = title.BlockText(source);
    return (message, children as ListBlock);
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(parameters).With(parameters), nameof(dialogBoxes).With(dialogBoxes)];
  }
}

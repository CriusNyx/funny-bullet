using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Markdig.Syntax;

/// <summary>
/// Defines a message presented to the player.
/// </summary>
public class DialogFileActorMessage : DialogFileActorBlockContent
{
  public string message;

  public DialogFileActorMessage(string message)
  {
    this.message = message;
  }

  public static DialogFileActorMessage Parse(ParagraphBlock paragraphBlock, string source)
  {
    return new DialogFileActorMessage(paragraphBlock.BlockText(source));
  }
}

/// <summary>
/// Defines a selector dialog where the player can select an option.
/// </summary>
public class DialogFileActorSelector : DialogFileActorBlockContent
{
  public string message;
  public string[] options;

  public DialogFileActorSelector(string message, string[] options)
  {
    this.message = message;
    this.options = options;
  }

  public static DialogFileActorSelector Parse(ListBlock block, string source)
  {
    throw new NotImplementedException();
  }
}

/// <summary>
/// Content of a dialog file actor block.
/// </summary>
public abstract class DialogFileActorBlockContent
{
  public static bool TryParse(
    MarkdownObject markdownObject,
    string source,
    out DialogFileActorBlockContent content
  )
  {
    if (markdownObject is ParagraphBlock paragraphBlock)
    {
      content = DialogFileActorMessage.Parse(paragraphBlock, source);
      return true;
    }
    else if (markdownObject is ListBlock listBlock)
    {
      content = DialogFileActorSelector.Parse(listBlock, source);
      return true;
    }
    content = null!;
    return false;
  }
}

/// <summary>
/// Dialog file actor
/// </summary>
public class DialogFileActorBlock : DialogFileSection
{
  public string actor;
  public string? label;
  public DialogFileActorBlockContent[] content;

  public DialogFileActorBlock(string actor, string? label, DialogFileActorBlockContent[] content)
  {
    this.actor = actor;
    this.label = label;
    this.content = content;
  }

  public static DialogFileActorBlock Parse(
    HeadingBlock heading,
    Queue<MarkdownObject> queue,
    string source
  )
  {
    var headingText = heading.BlockText(source);
    List<DialogFileActorBlockContent> contentList = new List<DialogFileActorBlockContent>();
    while (
      queue.TryPeek(out var next)
      && DialogFileActorBlockContent.TryParse(next, source, out var content)
    )
    {
      queue.Dequeue();
      contentList.Add(content);
    }
    return new DialogFileActorBlock(headingText.TrimStart('#').Trim(), null, contentList.ToArray());
  }
}

/// <summary>
/// A block with gd source code.
/// </summary>
public class DialogFileCodeBlock : DialogFileSection
{
  public string code;

  public static DialogFileCodeBlock Parse(CodeBlock codeBlock)
  {
    throw new NotImplementedException();
  }
}

/// <summary>
/// A section of a Dialog file.
/// </summary>
public abstract class DialogFileSection
{
  public static DialogFileSection? Parse(
    MarkdownObject blockHead,
    Queue<MarkdownObject> queue,
    string source
  )
  {
    if (blockHead is HeadingBlock headingBlock)
    {
      return DialogFileActorBlock.Parse(headingBlock, queue, source);
    }
    else if (blockHead is CodeBlock codeBlock)
    {
      return DialogFileCodeBlock.Parse(codeBlock);
    }
    else
    {
      return null;
    }
  }
}

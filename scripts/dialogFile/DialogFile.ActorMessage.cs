using Markdig.Syntax;

public partial class DialogFile
{
  /// <summary>
  /// Defines a message presented to the player.
  /// </summary>
  public class ActorMessage : ActorBlockContent
  {
    public string message;

    public ActorMessage(string message)
    {
      this.message = message;
    }

    public static ActorMessage Parse(ParagraphBlock paragraphBlock, string source)
    {
      return new ActorMessage(paragraphBlock.BlockText(source));
    }
  }
}

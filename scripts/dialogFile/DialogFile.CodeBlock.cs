using MDCodeBlock = Markdig.Syntax.CodeBlock;

public partial class DialogFile
{
  /// <summary>
  /// A block with gd source code.
  /// </summary>
  public class CodeBlock : Block
  {
    public string code;

    public CodeBlock(string code)
    {
      this.code = code;
    }

    public static CodeBlock Parse(MDCodeBlock codeBlock)
    {
      var code = codeBlock.Lines.ToString();
      return new CodeBlock(code);
    }
  }
}

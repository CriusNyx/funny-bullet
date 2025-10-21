using DeepEqual.Syntax;

namespace DialogTests;

public class DialogFileTests
{
  const string DialogTestFilesPath = "resources/DialogTestFiles";

  [DatapointSource]
  public (string name, DialogFile expected)[] CanParseDialogFileArgs =
  [
    ("Empty", new DialogFile()),
    ("WithActorBlock", new DialogFile { blocks = [new DialogFile.ActorBlock("Actor1", null, [])] }),
    (
      "WithActorMessage",
      new DialogFile
      {
        blocks =
        [
          new DialogFile.ActorBlock("Actor1", null, [new DialogFile.ActorMessage("Hello world!")]),
        ],
      }
    ),
    (
      "WithMultipleActors",
      new DialogFile
      {
        blocks =
        [
          new DialogFile.ActorBlock("Actor1", null, [new DialogFile.ActorMessage("Hello")]),
          new DialogFile.ActorBlock("Actor2", null, [new DialogFile.ActorMessage("Hi")]),
        ],
      }
    ),
    (
      "WithSelector",
      new DialogFile
      {
        blocks =
        [
          new DialogFile.ActorBlock(
            "Actor1",
            null,
            [new DialogFile.ActorSelector("Message", ["Option1", "Option2"])]
          ),
        ],
      }
    ),
    ("WithCode", new DialogFile { blocks = [new DialogFile.CodeBlock("var a = 5")] }),
    (
      "WithMeta",
      new DialogFile
      {
        blocks =
        [
          new DialogFile.DialogMeta
          {
            variables = new Dictionary<string, string> { { "hello", "World!" } },
          },
        ],
      }
    ),
  ];

  [Theory, Timeout(500)]
  public void CanParseDialogFile((string name, DialogFile expected) args)
  {
    var (name, expected) = args;
    var source = File.ReadAllText(Path.Join(DialogTestFilesPath, $"{name}.md"));
    var actual = DialogFile.Parse(source);
    actual.ShouldDeepEqual(expected);
  }
}

using DeepEqual.Syntax;

namespace DialogTests;

public class DialogFileMetaTests
{
  const string metaFilesDirectory = "resources/DialogMetaTestFiles";

  [DatapointSource]
  public (string name, DialogFile.DialogMeta expected)[] metaTestValues =
  [
    ("Empty", new DialogFile.DialogMeta()),
    ("WithEmptyActors", new DialogFile.DialogMeta()),
    ("WithEmptyVars", new DialogFile.DialogMeta()),
    ("WithError", null!),
    (
      "WithActor",
      new DialogFile.DialogMeta()
      {
        actors = new Dictionary<string, DialogFile.Actor>()
        {
          { "Foobar", new DialogFile.Actor() },
        },
      }
    ),
    (
      "WithActorGraphic",
      new DialogFile.DialogMeta()
      {
        actors = new Dictionary<string, DialogFile.Actor>()
        {
          {
            "Foobar",
            new DialogFile.Actor { graphic = "actor.png" }
          },
        },
      }
    ),
    (
      "WithActorSide",
      new DialogFile.DialogMeta()
      {
        actors = new Dictionary<string, DialogFile.Actor>()
        {
          {
            "Foobar",
            new DialogFile.Actor { side = PortraitSide.Left }
          },
        },
      }
    ),
    (
      "WithActorFlipped",
      new DialogFile.DialogMeta()
      {
        actors = new Dictionary<string, DialogFile.Actor>()
        {
          {
            "Foobar",
            new DialogFile.Actor { flipped = true }
          },
        },
      }
    ),
    (
      "WithVars",
      new DialogFile.DialogMeta { variables = new Dictionary<string, string> { { "foo", "bar" } } }
    ),
  ];

  [Theory]
  public void CanParseMeta((string name, DialogFile.DialogMeta expected) args)
  {
    var (name, expected) = args;
    var source = File.ReadAllText(Path.Join(metaFilesDirectory, $"{name}.yaml"));
    var actual = DialogFile.DialogMeta.Parse(source);
    actual.ShouldDeepEqual(expected);
  }
}

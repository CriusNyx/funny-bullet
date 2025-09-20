using System.Linq;
using Godot;

[GlobalClass]
public partial class Level : Node
{
  public override void _Ready()
  {
    var level = new SequenceNode("Level", this.GetChildrenNodes());
    LevelInterpreter.InterpretLevel(level);
    GD.Print(level.Debug());
  }
}

public interface LevelNode
{
  InterpreterNode GetNode();
}

public static class LevelNodeExtensions
{
  public static InterpreterNode[] GetChildrenNodes(this Node levelNode)
  {
    return levelNode.GetChildrenOfType<LevelNode>(false).Select(x => x.GetNode()).ToArray();
  }
}

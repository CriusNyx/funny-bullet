using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;

[GlobalClass]
public partial class LevelInterpreter : Node
{
  [Export]
  PackedScene? enemyPrefab;

  Queue<InterpreterNode> instructionStream = new Queue<InterpreterNode>();

  public static LevelInterpreter Instance => GameInstance.Instance.LevelInterpreter;

  public override void _Ready()
  {
    base._Ready();
  }

  public override void _Process(double delta)
  {
    StringBuilder sb = new StringBuilder();
    instructionStream = new Queue<InterpreterNode>(
      instructionStream.Where(level =>
      {
        var result = level.Update(delta) != LevelInterpreterResult.Done;
        sb.AppendLine(level.GetTreeStatus() + "\n");
        return result;
      })
    );

    Debugger.SetLevelInterpreterString(sb.ToString());
  }

  public static void InterpretLevel(InterpreterNode interpreterNode)
  {
    Instance.instructionStream.Enqueue(interpreterNode);
    interpreterNode.Start();
  }
}

using System.Collections.Generic;
using System.Reflection.Metadata;
using Godot;

public enum InputType
{
  Left,
  Right,
  Down,
  Up,
  Fire,
}

public class InputPoll
{
  private static Dictionary<InputType, Key> InputMap = new Dictionary<InputType, Key>()
  {
    { InputType.Left, Key.A },
    { InputType.Right, Key.D },
    { InputType.Down, Key.S },
    { InputType.Up, Key.W },
    { InputType.Fire, Key.Space },
  };

  public readonly IReadOnlyDictionary<InputType, bool> Inputs = new Dictionary<InputType, bool>();

  public InputPoll(Dictionary<InputType, bool> Inputs)
  {
    this.Inputs = Inputs;
  }

  public bool GetInput(InputType inputType)
  {
    return Inputs.Safe(inputType);
  }

  public Vector2 GetInputVector()
  {
    Vector2 output = Vector2.Zero;
    if (GetInput(InputType.Left))
    {
      output.X -= 1;
    }
    if (GetInput(InputType.Right))
    {
      output.X += 1;
    }
    if (GetInput(InputType.Down))
    {
      output.Y -= 1;
    }
    if (GetInput(InputType.Up))
    {
      output.Y += 1;
    }
    return output.Normalized();
  }

  public static InputPoll Empty()
  {
    var inputs = new Dictionary<InputType, bool>();
    foreach (var type in InputMap.Keys)
    {
      inputs.Add(type, false);
    }
    return new InputPoll(inputs);
  }

  public static InputPoll Current()
  {
    var inputs = new Dictionary<InputType, bool>();
    foreach (var (type, key) in InputMap)
    {
      inputs.Add(type, Input.IsPhysicalKeyPressed(key));
    }
    return new InputPoll(inputs);
  }
}

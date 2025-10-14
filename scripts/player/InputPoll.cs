using System;
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
  Accept,
}

public class InputPoll
{
  private static (InputType type, Key key)[] InputMap =
  [
    (InputType.Left, Key.A),
    (InputType.Right, Key.D),
    (InputType.Down, Key.S),
    (InputType.Up, Key.W),
    (InputType.Fire, Key.Space),
    (InputType.Accept, Key.E),
    (InputType.Accept, Key.Space),
  ];

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
    foreach (var type in Enum.GetValues<InputType>())
    {
      inputs.Add(type, false);
    }
    return new InputPoll(inputs);
  }

  public static InputPoll Current()
  {
    var inputs = new Dictionary<InputType, bool>();
    foreach (var type in Enum.GetValues<InputType>())
    {
      inputs.Add(type, false);
    }
    foreach (var (type, key) in InputMap)
    {
      if (Input.IsPhysicalKeyPressed(key))
      {
        inputs[type] = true;
      }
    }
    return new InputPoll(inputs);
  }
}

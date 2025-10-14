using Godot;

public class InputFrame
{
  public InputPoll Previous { get; private set; }
  public InputPoll Current { get; private set; }

  public InputFrame(InputPoll previous, InputPoll current)
  {
    Previous = previous;
    Current = current;
  }

  public Vector2 GetMovementVector()
  {
    return Current.GetInputVector();
  }

  public bool GetInput(InputType inputType)
  {
    return Current.GetInput(inputType);
  }

  public bool GetInputDown(InputType inputType)
  {
    return !Previous.GetInput(inputType) && Current.GetInput(inputType);
  }

  public bool GetInputUp(InputType inputType)
  {
    return Previous.GetInput(inputType) && !Current.GetInput(inputType);
  }

  public static InputFrame Poll(InputFrame previous)
  {
    return new InputFrame(previous?.Current ?? InputPoll.Empty(), InputPoll.Current());
  }

  public static InputFrame Empty()
  {
    return new InputFrame(InputPoll.Empty(), InputPoll.Empty());
  }
}

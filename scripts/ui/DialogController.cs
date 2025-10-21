using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class DialogController : Control
{
  const string PLAY_SPEED_FEILD = "play_speed";
  const string IS_ANIMATION_HOLDING_METHOD = "is_anim_holding";
  const string SET_BBCODE_METHOD = "set_bbcode";
  const string ADVANCE_METHOD = "advance";
  const float PLAY_SPEED = 30;
  const float PLAY_SPEED_FAST = 120;
  const string ANIMATION_FINISHED_SIGNAL = "anim_finished";
  const string CONTEXT_STATE_FIELD = "context_state";

  RichTextLabel dialogBox = null!;
  public event Action OnMessageFinished;

  public override void _EnterTree()
  {
    dialogBox = this.GetChildOfType<RichTextLabel>().NotNull();
    dialogBox.Connect(ANIMATION_FINISHED_SIGNAL, Callable.From(OnAnimationFinished));
    base._EnterTree();
  }

  public override void _Process(double delta)
  {
    if (IsAnimationHolding() && InputPoller.CurrentFrame.GetInputDown(InputType.Accept))
    {
      Advance();
    }
    else
    {
      SetPlaySpeed(
        InputPoller.CurrentFrame.GetInput(InputType.Accept) ? PLAY_SPEED_FAST : PLAY_SPEED
      );
    }

    base._Process(delta);
  }

  public void SetVars(IReadOnlyDictionary<string, string> vars)
  {
    var dictionary = (Godot.Collections.Dictionary)dialogBox.Get(CONTEXT_STATE_FIELD);
    dictionary.Clear();
    foreach (var (key, value) in vars)
    {
      dictionary[key] = value;
    }
  }

  public void PlayMessage(string message, bool appendH = true)
  {
    if (appendH)
    {
      message = $"[p] {message} [h] []";
    }
    dialogBox.Call(SET_BBCODE_METHOD, [message]);
  }

  private void SetPlaySpeed(float value)
  {
    dialogBox.Set(PLAY_SPEED_FEILD, value);
  }

  private void Advance()
  {
    dialogBox.Call(ADVANCE_METHOD);
  }

  private bool IsAnimationHolding()
  {
    return (bool)dialogBox.Call(IS_ANIMATION_HOLDING_METHOD);
  }

  private void OnAnimationFinished()
  {
    OnMessageFinished?.Invoke();
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

[GlobalClass]
public partial class DialogController : Control
{
  const string PLAY_SPEED_FEILD = "play_speed";
  const string BB_CODE_FIELD_NAME = "bbcode";
  const string IS_ANIMATION_HOLDING_METHOD = "is_anim_holding";
  const string ADVANCE_METHOD = "advance";
  const float PLAY_SPEED = 30;
  const float PLAY_SPEED_FAST = 120;
  const string ANIMATION_FINISHED_SIGNAL = "anim_finished";

  RichTextLabel dialogBox = null!;
  TaskCompletionSource taskCompletionSource = null!;

  public Task PlayDialogContent(DialogContent content, Dictionary<string, string>? vars)
  {
    if (content is IDialogMessage message)
    {
      var dict = new Godot.Collections.Dictionary<string, string>();
      if (vars != null)
      {
        foreach (var (key, value) in vars)
        {
          dict.Add(key, value);
        }
      }

      dialogBox.Set("context_state", dict);
      dialogBox.Call("set_bbcode", message.Message);
      dialogBox.Call("set_progress", 0);
      taskCompletionSource = new TaskCompletionSource();
      return taskCompletionSource.Task;
    }
    else
    {
      Debug.WarnUnexpectedType(typeof(DialogContent), content);
      throw new NotImplementedException();
    }
  }

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
    taskCompletionSource?.SetResult();
  }
}

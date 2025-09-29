using System;
using Godot;

[GlobalClass]
public partial class Level : Sequence
{
  public event Action<Level>? OnLevelFinished;

  public override void _Ready()
  {
    OnFinished += FinishLevel;
    Start();
  }

  public override void _Process(double delta)
  {
    Update(delta);
  }

  public void FinishLevel()
  {
    OnLevelFinished?.Invoke(this);
  }
}

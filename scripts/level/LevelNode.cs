using System;
using Godot;

public abstract partial class LevelNode : Node
{
  [Export]
  public bool IsDone { get; private set; }

  public event Action? OnFinished;

  public virtual void Start() { }

  public virtual void Update(double deltaTime) { }

  public void Finish()
  {
    IsDone = true;
    OnFinished?.Invoke();
  }
}

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Godot;

[Tool]
[GlobalClass]
public partial class Level : LevelSequence
{
  public event Action<Level>? OnLevelFinished;

  public override void _Ready()
  {
    OnFinished += FinishLevel;
    Start();
  }

  public override void _Process(double delta)
  {
    if (Engine.IsEditorHint())
    {
      DrawLevelBounds();
      DrawSafeBounds();
    }
    else
    {
      Update(delta);
    }
  }

  private void DrawLevelBounds()
  {
    DrawBounds(
      new Vector2(GameStats.GAMEBOARD_WIDTH, GameStats.GAMEBOARD_HEIGHT),
      new Color("white")
    );
  }

  private void DrawSafeBounds()
  {
    DrawBounds(
      new Vector2(GameStats.SAFE_SPACE_WIDTH, GameStats.SAFE_SPACE_HEIGHT),
      new Color("red")
    );
  }

  private void DrawBounds(Vector2 bounds, Color color)
  {
    var ll = (-bounds / 2).To3();
    var right = Vector3.Right * bounds.X;
    var up = Vector3.Up * bounds.Y;

    void DrawRay(Vector3 origin, Vector3 dir)
    {
      DebugDraw3D.DrawRay(origin, dir.Normalized(), dir.Length(), color);
    }

    // Bottom
    DrawRay(ll, right);
    // Left
    DrawRay(ll, up);
    // Right
    DrawRay(ll + right, up);
    // Top
    DrawRay(ll + up, right);
  }

  public void FinishLevel()
  {
    OnLevelFinished?.Invoke(this);
  }
}

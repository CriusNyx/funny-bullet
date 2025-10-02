using System.Runtime.CompilerServices;
using Godot;

public partial class SafeSpace : Node3D
{
  public static SafeSpace? Instance => Game.Instance?.SafeSpace;
  public Vector2 Size = new Vector2(
    GameStats.GAMEBOARD_WIDTH + GameStats.SAFE_SPACE_WIDTH * 2f,
    GameStats.GAMEBOARD_HEIGHT + GameStats.SAFE_SPACE_HEIGHT * 2f
  );

  public static bool Contains(Vector2 vector)
  {
    if (Instance == null)
    {
      return true;
    }
    return Instance.Size.EnclosesAsBounds(vector);
  }
}

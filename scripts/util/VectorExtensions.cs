using Godot;

public static class VectorExtensions
{
  public static Vector3 To3(this Vector2 vec)
  {
    return new Vector3(vec.X, vec.Y, 0);
  }
}

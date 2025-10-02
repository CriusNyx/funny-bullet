using Godot;

public static class VectorExtensions
{
  public static Vector2 To2(this Vector3 vec)
  {
    return new Vector2(vec.X, vec.Y);
  }

  public static Vector3 To3(this Vector2 vec, float z = 0)
  {
    return new Vector3(vec.X, vec.Y, z);
  }

  public static bool EnclosesAsBounds(this Vector2 vec, Vector2 point)
  {
    var half = vec / 2f;
    return point.X >= -half.X && point.X <= half.X && point.Y >= -half.Y && point.Y <= half.Y;
  }
}

using Godot;

public static class VecMath
{
  public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
  {
    return a.Lerp(b, t);
  }
}

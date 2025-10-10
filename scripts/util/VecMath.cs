using Godot;

public static class VecMath
{
  public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
  {
    return a.Lerp(b, t);
  }

  public static Vector3 SClamp(this Vector3 v, float l)
  {
    if (v.Length() > l)
    {
      return v.Normalized() * l;
    }
    return v;
  }
}

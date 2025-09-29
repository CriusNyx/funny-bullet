using Godot;

[GlobalClass]
public abstract partial class Spline : Resource
{
  public abstract Vector2 Get(float t);
}

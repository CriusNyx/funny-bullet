using Godot;

[GlobalClass]
public partial class ScreenParabola : Spline
{
  /// <summary>
  /// With width of the parabola relative to the screen width, and the depth. 0 is no width. 1 is
  /// the width of the whole screen.
  /// </summary>
  [Export(PropertyHint.Range, "0, 1")]
  public float width;

  /// <summary>
  /// The depth of the focus in screen space.
  /// 0 is the top of the screen, and 1 is the bottom.
  /// </summary>
  [Export(PropertyHint.Range, "0, 1")]
  public float focusDepth;

  public override Vector2 Get(float t)
  {
    throw new System.NotImplementedException();
  }
}

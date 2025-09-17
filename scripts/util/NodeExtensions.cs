using Godot;

public static class NodeExtensions
{
  public static T AppendChild<T>(this Node node, T child)
    where T : Node
  {
    node.AddChild(child);
    return child;
  }

  public static T WithTransform<T>(this T node, Vector3? position)
    where T : Node3D
  {
    if (position != null)
    {
      node.Position = position.Value;
    }
    return node;
  }

  public static T WithParent<T>(this T node, Node parent)
    where T : Node
  {
    parent?.AddChild(node);
    return node;
  }

  public static T WithName<T>(this T node, string name)
    where T : Node
  {
    node.Name = name;
    return node;
  }
}

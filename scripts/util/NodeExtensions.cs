using System.Collections.Generic;
using System.Linq;
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

  public static T? GetParentOfType<T>(this Node node)
    where T : GodotObject
  {
    if (node is T t)
    {
      return t;
    }
    return node.GetParent()?.GetParentOfType<T>();
  }

  public static IEnumerable<T> GetParentsOfType<T>(this Node node)
  {
    for (var self = node; self != null; self = self?.GetParent())
    {
      if (self is T t)
      {
        yield return t;
      }
    }
  }

  public static T? GetChildOfType<T>(this Node node)
  {
    foreach (var child in node.GetChildren())
    {
      if (child is T t)
      {
        return t;
      }
    }
    foreach (var child in node.GetChildren())
    {
      if (child.GetChildOfType<T>() is T t)
      {
        return t;
      }
    }
    return default;
  }

  public static IEnumerable<T> GetChildrenOfType<T>(this Node node, bool recursive = true)
  {
    foreach (var child in node.GetChildren())
    {
      if (child is T t)
      {
        yield return t;
      }
      if (recursive)
      {
        foreach (var second in child.GetChildrenOfType<T>(recursive))
        {
          yield return second;
        }
      }
    }
  }
}

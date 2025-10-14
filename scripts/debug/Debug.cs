using System;
using System.Diagnostics;
using Godot;

public static class Debug
{
  public static string ThisMethodName()
  {
    return new StackTrace()
      .GetFrame(1)
      .NotNull()
      .GetMethod()
      .NotNull()
      .Transform(method => $"${method.DeclaringType?.Name}.{method.Name}");
  }

  public static string MethodLine(int stackDepth = 1)
  {
    var trace = new StackTrace().GetFrame(stackDepth).NotNull();
    var method = trace.GetMethod().NotNull();
    return $"{method.DeclaringType.NotNull().Name}.{method.Name}:{trace.GetFileLineNumber()}";
  }

  public static void PrintMethod()
  {
    GD.Print(MethodLine(2));
  }

  public static void WarnUnexpectedType(Type expectedType, object unexpectedObject)
  {
    GD.PushWarning(
      $"Unexpected {expectedType.Name}: {unexpectedObject.GetType().Name}",
      unexpectedObject.Debug()
    );
  }
}

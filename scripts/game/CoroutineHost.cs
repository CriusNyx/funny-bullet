using System.Collections;
using System.Collections.Generic;
using Godot;

public partial class CoroutineHost : Node
{
  Queue<Coroutine> runningRoutines = new Queue<Coroutine>();
  public static CoroutineHost Instance = Game.Instance.CoroutineHost;
  public static double DeltaTime { get; private set; }

  public Coroutine StartCoroutine(Node host, IEnumerator routine)
  {
    var output = new Coroutine(host.GetInstanceId(), routine);
    runningRoutines.Enqueue(output);
    return output;
  }

  public override void _Process(double delta)
  {
    DeltaTime = delta;
    var routines = runningRoutines.ToArray();
    runningRoutines.Clear();
    foreach (var routine in routines)
    {
      if (routine.Update())
      {
        runningRoutines.Enqueue(routine);
      }
    }
  }
}

public static class CoroutineExtensions
{
  public static Coroutine StartCoroutine(this Node node, IEnumerable routine)
  {
    return CoroutineHost.Instance.StartCoroutine(node, routine.GetEnumerator());
  }

  public static Coroutine StartCoroutine(this Node node, IEnumerator routine)
  {
    return CoroutineHost.Instance.StartCoroutine(node, routine);
  }
}

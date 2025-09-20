using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public class ScheduledTask
{
  public readonly string taskName;
  public readonly IEnumerator taskStream;
  public readonly TaskCompletionSource completionSource;

  public ScheduledTask(
    string taskName,
    IEnumerator taskStream,
    TaskCompletionSource completionSource
  )
  {
    this.taskName = taskName;
    this.taskStream = taskStream;
    this.completionSource = completionSource;
  }
}

public class Scheduler
{
  private List<ScheduledTask> tasks = new List<ScheduledTask>();
  public static Scheduler Instance => GameInstance.Scheduler;

  public void Process()
  {
    lock (tasks)
    {
      Stack<int> completedTasks = new Stack<int>();
      foreach (var (task, index) in tasks.WithIndex())
      {
        if (!task.taskStream.MoveNext())
        {
          task.completionSource.SetResult();
          completedTasks.Push(index);
        }
      }

      while (completedTasks.Count > 0)
      {
        tasks.RemoveAt(completedTasks.Pop());
      }
    }
  }

  public static Task OnMainThread(string taskName, Action action)
  {
    return Instance._OnMainThread(taskName, action);
  }

  public Task _OnMainThread(string taskName, Action action)
  {
    var cs = new TaskCompletionSource();

    lock (tasks)
    {
      tasks.Add(new ScheduledTask(taskName, action.ToEnumerable().GetEnumerator(), cs));
    }
    return cs.Task;
  }
}

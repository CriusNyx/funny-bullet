using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class TaskExtensions
{
  public static async Task Then(this Task task, Func<Task> after)
  {
    await task;
    await after();
  }

  public static async Task Then(this Task task, Action after)
  {
    await task;
    after();
  }

  public static async Task Sequence(params Func<Task>[] tasks)
  {
    foreach (var task in tasks)
    {
      await task();
    }
  }

  public static async Task Sequence(IEnumerable<Func<Task>> tasks)
  {
    foreach (var task in tasks)
    {
      await task();
    }
  }

  public static Task Wait(float seconds)
  {
    return Task.Run(() =>
    {
      Thread.Sleep((int)(seconds / 1000));
    });
  }
}

public static class TaskHelper
{
  public static Task<T> RunWithTimeout<T>(Func<T> func, int miliseconds)
  {
    var cancellationSource = new CancellationTokenSource();
    var result = Task.Run(func, cancellationSource.Token);
    cancellationSource.CancelAfter(miliseconds);
    return result;
  }
}

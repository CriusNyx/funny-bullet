using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCoroutine;

public static class CoroutineExtensions
{
  public static Task AsTask(this Coroutine coroutine)
  {
    TaskCompletionSource taskSource = new TaskCompletionSource();

    IEnumerator<ulong> TaskRoutine()
    {
      yield return Coroutine.WaitForOtherCoroutine(coroutine);
      taskSource.SetResult();
    }

    Coroutine.StartCoroutine(TaskRoutine());
    return taskSource.Task;
  }
}

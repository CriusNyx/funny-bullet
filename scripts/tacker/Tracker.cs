public class Tracker
{
  public bool IsDone { get; private set; } = false;

  public void Free()
  {
    IsDone = true;
  }
}

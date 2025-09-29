using System.Collections.Generic;
using System.Linq;

public class TrackerPool
{
  public int TrackerCount => trackers.Count(x => !x.IsDone);

  List<Tracker> trackers = new List<Tracker>();

  public void RegisterTracker(Tracker tracker)
  {
    trackers.Add(tracker);
  }
}

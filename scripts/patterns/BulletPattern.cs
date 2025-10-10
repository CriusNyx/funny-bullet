using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Godot;

[Tool]
[GlobalClass]
public abstract partial class BulletPattern : Node
{
  public abstract IEnumerable<BulletSpawn> Process(
    IEnumerable<IEnumerable<BulletSpawn>> childSpawns
  );

  public IEnumerable<BulletSpawn> ProcessSelf()
  {
    return Process(this.GetChildrenOfType<BulletPattern>().Select(x => x.ProcessSelf()));
  }

  [ExportToolButton("Preview Pattern")]
  public Callable PreviewSelfCallable => Callable.From(PreviewSelf);

  [ExportToolButton("Preview Root")]
  public Callable PreviewRootCallable => Callable.From(() => this.GetRootOfType().PreviewSelf());

  public void PreviewSelf()
  {
    var spawn = ProcessSelf().ToArray();
    IterateDraw(spawn, 0);
  }

  public async void IterateDraw(IEnumerable<BulletSpawn> bullets, int iterationCount)
  {
    DrawForTimestamp(bullets, iterationCount * 0.01f);
    await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
    if (iterationCount < 1000)
    {
      IterateDraw(bullets, iterationCount + 1);
    }
  }

  public void DrawForTimestamp(IEnumerable<BulletSpawn> bullets, float time)
  {
    foreach (var bullet in bullets)
    {
      if (bullet.spawnTime <= time)
      {
        float lifetime = time - bullet.spawnTime;
        var pos = bullet.Interpolate(lifetime);
        DebugDraw3D.DrawRay(
          pos.To3(),
          (-bullet.Direction()).To3(),
          0.1f,
          new Color("white"),
          0.01f
        );
      }
    }
  }
}

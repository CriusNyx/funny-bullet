using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

[GlobalClass]
[Tool]
public abstract partial class BulletPattern : Node
{
  public abstract IEnumerable<BulletSpawn> Process(IEnumerable<BulletSpawn> childSpawns);

  public IEnumerable<BulletSpawn> ProcessSelf()
  {
    return Process(this.GetChildrenOfType<BulletPattern>().SelectMany(x => x.ProcessSelf()));
  }

  [ExportToolButton("Preview Pattern")]
  Callable PreviewSelfAction => Callable.From(PreviewSelf);

  public void PreviewSelf() { }
}

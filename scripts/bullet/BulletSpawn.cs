using System.Collections.Generic;
using System.Linq;
using Godot;

public struct BulletSpawn : DebugPrint
{
  public Vector2 position;
  public float spawnTime;
  public float speed;
  public float angle;
  public float lifetime;

  public BulletSpawn WithParameters(SpawnParameters spawnParameters)
  {
    return Clone(angle: angle + spawnParameters.angle);
  }

  public BulletSpawn Clone(
    Vector2? position = null,
    float? spawnTime = null,
    float? speed = null,
    float? angle = null,
    float? lifetime = null
  )
  {
    return new BulletSpawn
    {
      position = position ?? this.position,
      spawnTime = spawnTime ?? this.spawnTime,
      speed = speed ?? this.speed,
      angle = angle ?? this.angle,
      lifetime = lifetime ?? this.lifetime,
    };
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [("spawnTime", spawnTime), ("speed", speed), ("angle", angle), ("lifetime", lifetime)];
  }

  public static IEnumerable<BulletSpawn> Sequence(
    float delay,
    params IEnumerable<BulletSpawn>[] spawns
  )
  {
    return spawns.Skip(1).Aggregate(spawns.First(), (prev, curr) => prev.ThenSpawn(curr, delay));
  }

  public static IEnumerable<BulletSpawn> Sequence(
    float delay,
    IEnumerable<IEnumerable<BulletSpawn>> spawns
  )
  {
    return spawns.Aggregate<IEnumerable<BulletSpawn>, IEnumerable<BulletSpawn>>(
      [],
      (prev, curr) => prev.ThenSpawn(curr, delay)
    );
  }
}

public static class BulletSpawnExtensions
{
  public static IEnumerable<BulletSpawn> Fan(
    this BulletSpawn bullet,
    float angle,
    int count,
    float delayPerBullet = 0
  )
  {
    float halfA = angle / 2;
    return Enumerators
      .Range(count)
      .Select(i =>
        bullet.Clone(
          angle: bullet.angle - halfA + angle * ((float)i / (count - 1)),
          spawnTime: bullet.spawnTime + delayPerBullet * i
        )
      );
  }

  public static IEnumerable<BulletSpawn> Fan(
    this IEnumerable<BulletSpawn> bullet,
    float angle,
    int count,
    float delayPerBullet = 0
  )
  {
    return bullet.SelectMany(x => x.Fan(angle, count, delayPerBullet));
  }

  public static IEnumerable<BulletSpawn> InterleavedFan(
    this BulletSpawn bullet,
    int repeatCount,
    float outerAngle,
    int count,
    float delayPerVolley,
    bool withLastFan = false
  )
  {
    float innerAngle = outerAngle / (count - 1) * (count - 2);

    return BulletSpawn
      .Sequence(
        delayPerVolley,
        bullet.Fan(outerAngle, count, 0),
        bullet.Fan(innerAngle, count - 1, 0)
      )
      .RepeatSequence(repeatCount, delayPerVolley)
      .If(withLastFan, x => x.ThenSpawn(bullet.Fan(outerAngle, count, 0), delayPerVolley));
  }

  public static IEnumerable<BulletSpawn> Repeat(
    this BulletSpawn bullet,
    int repeatCount,
    float delay
  )
  {
    return Enumerators
      .Range(repeatCount)
      .Select(i => bullet.Clone(spawnTime: bullet.spawnTime + delay * i));
  }

  public static IEnumerable<BulletSpawn> Repeat(
    this IEnumerable<BulletSpawn> bullets,
    int repeatCount,
    float delay
  )
  {
    return bullets.SelectMany(x => x.Repeat(repeatCount, delay));
  }

  public static IEnumerable<BulletSpawn> RepeatSequence(
    this IEnumerable<BulletSpawn> bullets,
    int repeatCount,
    float delay
  )
  {
    return BulletSpawn.Sequence(delay, Enumerators.Range(repeatCount).Select(x => bullets));
  }

  public static BulletSpawn Delay(this BulletSpawn bullet, float delay)
  {
    return bullet.Clone(spawnTime: bullet.spawnTime + delay);
  }

  public static IEnumerable<BulletSpawn> Delay(this IEnumerable<BulletSpawn> bullet, float delay)
  {
    return bullet.Select(x => x.Delay(delay));
  }

  public static IEnumerable<BulletSpawn> ThenSpawn(
    this IEnumerable<BulletSpawn> start,
    IEnumerable<BulletSpawn> then,
    float delay = 0
  )
  {
    var lastSpawnTime = start.SafeMax(x => x.spawnTime) ?? 0;
    return start.Concat(then.Delay(lastSpawnTime + delay));
  }
}

using System;

[Flags]
public enum DamageFiler
{
  player = 1 << 0,
  enemy = 1 << 1,
}

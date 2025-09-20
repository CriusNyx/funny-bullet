using System;
using Godot;

public partial class OnDeath : Node, HandleKilled
{
  Action<Character> handler;

  public OnDeath(Action<Character> handler)
  {
    this.handler = handler;
  }

  public void OnKilled(Character character)
  {
    handler?.Invoke(character);
  }
}

using DeepEqual.Syntax;
using FunnyBullet.Dialog;

namespace DialogTests;

enum EventType
{
  PlayMessage,
  StartActor,
  StopActor,
  InterpretCode,
}

class DialogPlayerTestImplementation : DialogPlayerImplementation
{
  private List<(EventType type, object args)> events = new List<(EventType, object)>();
  public EventType[] EventTypes => events.Select(x => x.type).ToArray();
  public bool autoAdvance = false;

  public void PlayMessage(DialogPlayer player, PlayMessageArgs args)
  {
    LogEvent(EventType.PlayMessage, args);
    if (autoAdvance)
    {
      player.OnMessageFinished();
    }
  }

  public void StartActor(DialogPlayer player, StartActorArgs args)
  {
    LogEvent(EventType.StartActor, args);
  }

  public void StopActor(DialogPlayer player, StopActorArgs args)
  {
    LogEvent(EventType.StopActor, args);
  }

  public void InterpretCode(DialogPlayer player, InterpretCodeArgs args)
  {
    LogEvent(EventType.InterpretCode, args);
    if (autoAdvance)
    {
      player.OnCodeFinished(args.codeBlock);
    }
  }

  private void LogEvent(EventType type, object args)
  {
    events.Add((type, args));
  }

  public void OnDialogFinished(DialogPlayer player)
  {
    throw new NotImplementedException();
  }
}

public class DialogPlayerTests
{
  const string DialogPlayerTestFilesPath = "resources/DialogPlayerTestFiles";

  private DialogFile OpenFile(string fileName)
  {
    return DialogFile.Parse(
      File.ReadAllText(Path.Join(DialogPlayerTestFilesPath, $"{fileName}.md"))
    );
  }

  [Test]
  public void CanPlayEmptyDialog()
  {
    var implementation = new DialogPlayerTestImplementation { autoAdvance = true };
    var player = new DialogPlayer(implementation);
    player.SetDialogFile(OpenFile("Empty"));
    var expected = new EventType[] { };
    expected.ShouldDeepEqual(implementation.EventTypes);
  }

  [Test]
  public void CanPlayDialogWithMessage()
  {
    var implementation = new DialogPlayerTestImplementation { autoAdvance = true };
    var player = new DialogPlayer(implementation);
    player.SetDialogFile(OpenFile("WithMessage"));
    EventType[] expected = [EventType.StartActor, EventType.PlayMessage, EventType.StopActor];
    expected.ShouldDeepEqual(implementation.EventTypes);
  }

  [Test]
  public void CanPlayDialogWithMultipleMessage()
  {
    var implementation = new DialogPlayerTestImplementation { autoAdvance = true };
    var player = new DialogPlayer(implementation);
    player.SetDialogFile(OpenFile("WithMultipleMessages"));
    EventType[] expected =
    [
      EventType.StartActor,
      EventType.PlayMessage,
      EventType.PlayMessage,
      EventType.StopActor,
    ];
    expected.ShouldDeepEqual(implementation.EventTypes);
  }

  [Test]
  public void CanPlayDialogWithMultipleActors()
  {
    var implementation = new DialogPlayerTestImplementation { autoAdvance = true };
    var player = new DialogPlayer(implementation);
    player.SetDialogFile(OpenFile("WithMultipleActors"));
    EventType[] expected =
    [
      EventType.StartActor,
      EventType.PlayMessage,
      EventType.StopActor,
      EventType.StartActor,
      EventType.PlayMessage,
      EventType.StopActor,
    ];
    expected.ShouldDeepEqual(implementation.EventTypes);
  }

  [Test]
  public void CanPlayDialogWithCode()
  {
    var implementation = new DialogPlayerTestImplementation { autoAdvance = true };
    var player = new DialogPlayer(implementation);
    player.SetDialogFile(OpenFile("WithCode"));
    EventType[] expected = [EventType.InterpretCode];
    expected.ShouldDeepEqual(implementation.EventTypes);
  }
}

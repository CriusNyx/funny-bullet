using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FunnyBullet.Dialog;

public struct StartActorArgs
{
  public DialogFile dialogFile;
  public DialogFile.ActorBlock actorBlock;
  public DialogFile.Actor actor;
}

public struct StopActorArgs
{
  public DialogFile dialogFile;
  public DialogFile.ActorBlock actorBlock;
  public DialogFile.Actor actor;
}

public struct PlayMessageArgs
{
  public DialogFile dialogFile;
  public DialogFile.ActorBlock actorBlock;
  public DialogFile.Actor actor;
  public DialogFile.ActorMessage message;
}

public struct InterpretCodeArgs
{
  public DialogFile dialogFile;
  public DialogFile.CodeBlock codeBlock;
}

public interface DialogPlayerImplementation
{
  public void OnMeta(DialogPlayer player, DialogFile.DialogMeta meta);
  public void StartActor(DialogPlayer player, StartActorArgs args);

  public void StopActor(DialogPlayer player, StopActorArgs args);

  public void PlayMessage(DialogPlayer player, PlayMessageArgs args);
  public void InterpretCode(DialogPlayer player, InterpretCodeArgs args);
  public void OnDialogFinished(DialogPlayer player);
}

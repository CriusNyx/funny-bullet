using System;

namespace FunnyBullet.Dialog;

struct DialogState
{
  public DialogFile? file;
  public int currentBlockIndex;
  public int currentContentIndex;

  public DialogState Clone(
    DialogFile? file = null,
    int? currentBlockIndex = null,
    int? currentContentIndex = null
  )
  {
    return new DialogState
    {
      file = file ?? this.file,
      currentBlockIndex = currentBlockIndex ?? this.currentBlockIndex,
      currentContentIndex = currentContentIndex ?? this.currentContentIndex,
    };
  }

  public DialogFile.Block? CurrentBlock => file?.blocks?.Safe(currentBlockIndex);
  public DialogFile.ActorBlockContent? CurrentContent =>
    CurrentBlock?.As<DialogFile.ActorBlock>()?.content.Safe(currentContentIndex);

  public DialogFile.Actor? CurrentActor
  {
    get
    {
      if (CurrentBlock is DialogFile.ActorBlock actorBlock)
      {
        return file?.Meta?.actors.Safe(actorBlock.actorName);
      }
      return null;
    }
  }

  public DialogState Next()
  {
    // Advance section
    if (CurrentBlock is DialogFile.ActorBlock actorBlock)
    {
      var nextSection = currentContentIndex + 1;
      if (actorBlock.content.HasIndex(nextSection))
      {
        return Clone(currentContentIndex: nextSection);
      }
    }

    // Advance block
    var nextBlock = currentBlockIndex + 1;
    if (file?.blocks?.HasIndex(nextBlock) ?? false)
    {
      return Clone(currentBlockIndex: nextBlock, currentContentIndex: 0);
    }

    // Return an empty dialog state.
    return new DialogState();
  }
}

public class DialogPlayer
{
  DialogPlayerImplementation implementation;
  Observable<DialogState> state;

  public DialogPlayer(DialogPlayerImplementation implementation)
  {
    this.implementation = implementation;
    state = new Observable<DialogState>(OnChange: HandleStateChange);
  }

  public void SetDialogFile(DialogFile dialogFile)
  {
    state.Value = new DialogState { file = dialogFile };
  }

  void HandleStateChange(DialogState current, DialogState previous)
  {
    if (current.CurrentBlock != previous.CurrentBlock)
    {
      UnmountPreviousBlock(previous);
      MountCurrentBlock(current);
    }
    if (current.CurrentContent != previous.CurrentContent)
    {
      MountContent(current);
    }
    if (current.file == null && previous.file != null)
    {
      implementation.OnDialogFinished(this);
    }
  }

  private void UnmountPreviousBlock(DialogState previous)
  {
    if (previous.CurrentBlock is DialogFile.ActorBlock actorBlock)
    {
      implementation.StopActor(
        this,
        new StopActorArgs
        {
          dialogFile = previous.file.NotNull(),
          actorBlock = actorBlock,
          actor = previous.CurrentActor.NotNull(),
        }
      );
    }
  }

  private void MountCurrentBlock(DialogState current)
  {
    var currentBlock = current.CurrentBlock;
    if (currentBlock is DialogFile.ActorBlock actorBlock)
    {
      implementation.StartActor(
        this,
        new StartActorArgs
        {
          dialogFile = current.file.NotNull("file"),
          actorBlock = actorBlock,
          actor = current.CurrentActor.NotNull("CurrentActor"),
        }
      );
    }
    else if (currentBlock is DialogFile.CodeBlock codeBlock)
    {
      implementation.InterpretCode(
        this,
        new InterpretCodeArgs { dialogFile = current.file.NotNull(), codeBlock = codeBlock }
      );
    }
    else if (currentBlock is DialogFile.DialogMeta meta)
    {
      implementation.OnMeta(this, meta);
      AdvanceNextState();
    }
  }

  private void MountContent(DialogState current)
  {
    var content = current.CurrentContent;
    if (content is DialogFile.ActorMessage message)
    {
      implementation.PlayMessage(
        this,
        new PlayMessageArgs
        {
          dialogFile = current.file.NotNull(),
          actorBlock = current.CurrentBlock.NotNull().As<DialogFile.ActorBlock>().NotNull(),
          actor = current.CurrentActor.NotNull(),
          message = message,
        }
      );
    }
    else if (content is DialogFile.ActorSelector selector)
    {
      throw new NotImplementedException();
    }
  }

  private void AdvanceNextState()
  {
    state.Value = state.Value.Next();
  }

  public void OnMessageFinished()
  {
    AdvanceNextState();
  }

  public void OnCodeFinished(DialogFile.CodeBlock codeBlock)
  {
    if (state.Value.CurrentBlock == codeBlock)
    {
      AdvanceNextState();
    }
  }

  public void Jump(string label)
  {
    int blockIndex = state.Value.file?.GetBlockIndexWithLabel(label) ?? -1;
    state.Value = state.Value.Clone(currentBlockIndex: blockIndex, currentContentIndex: 0);
  }
}

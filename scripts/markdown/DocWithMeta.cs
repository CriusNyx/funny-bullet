using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StringEnumerator(string source) : IEnumerator<string>
{
  StringReader sr = new StringReader(source);

  public string Current { get; private set; } = null!;

  object IEnumerator.Current => Current;

  public void Dispose()
  {
    sr.Dispose();
  }

  public bool MoveNext()
  {
    Current = sr.ReadLine()!;
    return Current != null;
  }

  public void Reset()
  {
    throw new NotImplementedException();
  }
}

public class DocWithMeta(string? meta, string? text) : DebugPrint
{
  public string? Meta => meta;
  public string? Text = text;

  public static DocWithMeta ParseString(string source)
  {
    var sr = new StringEnumerator(source);
    string? meta = null;
    if (TryLexMetaBlock(sr, out var block))
    {
      meta = block;
    }

    return new DocWithMeta(meta, sr.Rest().StringJoin("\n"));
  }

  private static bool TryLexMetaBlock(IEnumerator<string> sr, out string block)
  {
    sr.TakeWhile(x => x.Trim() == "");
    block = null!;
    if (sr.Take().Trim() != "---")
    {
      return false;
    }
    block = sr.TakeWhile((x) => x.Trim() != "---").StringJoin("\n");

    return sr.Current == "---";
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(meta).With(Meta)!, nameof(text).With(Text)!];
  }
}

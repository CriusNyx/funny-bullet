using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Callbacks;

public partial class DialogFile
{
  [YamlSerializable]
  public class Actor
  {
    [YamlMember]
    public string? graphic;

    [YamlMember]
    public PortraitSide? side;

    [YamlMember]
    public bool? flipped;
  }

  [YamlSerializable]
  public class DialogMeta : Block
  {
    private static IDeserializer deserializer = new DeserializerBuilder().Build();

    [YamlMember]
    public Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

    [YamlMember]
    public Dictionary<string, string> variables = new Dictionary<string, string>();

    public static DialogMeta? Parse(string source)
    {
      return deserializer.Deserialize<DialogMeta>(source);
    }

    [OnDeserialized]
    private void OnDeserialized()
    {
      actors = actors ?? new Dictionary<string, Actor>();
      actors.InitializeDictionary(() => new Actor());
      variables = variables ?? new Dictionary<string, string>();
      variables.InitializeDictionary(() => "");
    }
  }
}

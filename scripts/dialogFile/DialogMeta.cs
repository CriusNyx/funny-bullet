using System.Collections.Generic;
using YamlDotNet.Serialization;

[YamlSerializable]
public class DialogActor
{
  [YamlMember]
  public string? graphic;

  [YamlMember]
  public PortraitSide? side;
}

[YamlSerializable]
public class DialogMeta
{
  private static IDeserializer deserializer = new DeserializerBuilder().Build();

  [YamlMember]
  public Dictionary<string, DialogActor>? actors;

  [YamlMember]
  public Dictionary<string, string>? variables;

  public static DialogMeta? Parse(string source)
  {
    return Functional.Safe(() => deserializer.Deserialize<DialogMeta>(source));
  }
}

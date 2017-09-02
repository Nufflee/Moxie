using System;

namespace Moxie.Server
{
  [Serializable]
  public class MessagePacket : IPacket
  {
    public readonly string name;
    public readonly string message;

    public MessagePacket(string name, string message)
    {
      this.name = name;
      this.message = message;
    }
  }
}
using System;

namespace Moxie.Server
{
  [Serializable]
  public class MessagePacket : Packet
  {
    public readonly string name;
    public readonly string message;

    public MessagePacket(string name, string message)
    {
      this.name = name;
      this.message = message;
    }

    public override string ToString()
    {
      return $"{DateTime.Now.ToShortTimeString()} {name}: {message}";
    }
  }
}
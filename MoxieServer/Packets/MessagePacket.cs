using System;
using Moxie.Common;

namespace Moxie.Server
{
  [Serializable]
  public class MessagePacket : Packet
  {
    public readonly User user;
    public readonly string message;

    public MessagePacket(User user, string message)
    {
      this.user = user;
      this.message = message;
    }

    public override string ToString()
    {
      return $"{DateTime.Now.ToShortTimeString()} {user.name}: {message}";
    }
  }
}
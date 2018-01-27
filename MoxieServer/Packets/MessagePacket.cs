using System;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class MessagePacket : Packet
  {
    public User User { get; }
    public string Message { get; }
    public DateTime SendTime { get; }

    public MessagePacket(User user, string message)
    {
      User = user;
      Message = message;
      SendTime = DateTime.Now;
    }

    public override string ToString()
    {
      return $"{SendTime.ToShortTimeString()} {User.Name}: {Message}";
    }
  }
}
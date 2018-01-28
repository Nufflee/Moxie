using System;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class MessagePacket : TextPacket
  {
    public User User { get; }

    public MessagePacket(User user, string message)
      : base(message, user.Ip)
    {
      User = user;
      FormattedText = $"{SendTime.ToShortTimeString()} {User.Name}: {Text}";
    }
  }
}
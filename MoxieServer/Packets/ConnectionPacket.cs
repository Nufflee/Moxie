using System;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class ConnectionPacket : Packet
  {
    public User User { get; }
    public override IP4 Sender => User.Ip;

    public ConnectionPacket(User user)
    {
      User = user;
    }
  }
}
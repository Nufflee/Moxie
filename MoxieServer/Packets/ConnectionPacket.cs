using System;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class ConnectionPacket : Packet
  {
    public User User { get; }
    public IP4 Ip { get; }

    public ConnectionPacket(User user, IP4 ip)
    {
      User = user;
      Ip = ip;
    }
  }
}
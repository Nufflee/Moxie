using System;
using Moxie.Common;

namespace Moxie.Server
{
  [Serializable]
  public class ConnectionPacket : Packet
  {
    public readonly User user;
    public readonly IP4 ip;

    public ConnectionPacket(User user, IP4 ip)
    {
      this.user = user;
      this.ip = ip;
    }
  }
}
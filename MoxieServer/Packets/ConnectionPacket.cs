using System;
using Moxie.Common;

namespace Moxie.Server
{
  [Serializable]
  public class ConnectionPacket : Packet
  {
    public readonly string name;
    public readonly IP4 ip;

    public ConnectionPacket(string name, IP4 ip)
    {
      this.name = name;
      this.ip = ip;
    }
  }
}
using System;
using System.Collections.Generic;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class ListPacket<T> : Packet
    where T : Packet
  {
    public List<T> Packets { get; }
    public override IP4 Sender { get; }

    public ListPacket(List<T> packets, IP4 sender)
    {
      Packets = packets;
      Sender = sender;
    }
  }
}
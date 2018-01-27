using System;
using System.Collections.Generic;

namespace Moxie.Server.Packets
{
  [Serializable]
  public class ListPacket<T> : Packet
    where T : Packet
  {
    public List<T> Packets { get; }

    public ListPacket(List<T> packets)
    {
      Packets = packets;
    }
  }
}
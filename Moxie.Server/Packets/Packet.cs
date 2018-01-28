using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Moxie.Common;

namespace Moxie.Server.Packets
{
  [Serializable]
  public abstract class Packet
  {
    public abstract IP4 Sender { get; }

    public byte[] Serialize()
    {
      BinaryFormatter formatter = new BinaryFormatter();

      byte[] data;

      using (MemoryStream stream = new MemoryStream())
      {
        formatter.Serialize(stream, this);
        data = stream.ToArray();
      }

      return data;
    }

    public static object Deserialize(byte[] data)
    {
      BinaryFormatter formatter = new BinaryFormatter();

      using (MemoryStream stream = new MemoryStream())
      {
        stream.Write(data, 0, data.Length);
        stream.Seek(0, SeekOrigin.Begin);

        return formatter.Deserialize(stream);
      }
    }
  }
}
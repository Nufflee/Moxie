using System;
using System.Net;

namespace Moxie.Common
{
  [Serializable]
  public struct IP4
  {
    public string Address { get; }
    public int Port { get; }

    public IP4(string address, int port)
    {
      Address = address;
      Port = port;
    }

    public static implicit operator IPEndPoint(IP4 ip)
    {
      return new IPEndPoint(IPAddress.Parse(ip.Address), ip.Port);
    }

    public static implicit operator IP4(IPEndPoint endPoint)
    {
      return new IP4(endPoint.Address.ToString(), endPoint.Port);
    }
  }
}
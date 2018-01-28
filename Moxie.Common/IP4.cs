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

    public static bool operator ==(IP4 left, IP4 right)
    {
      return left.Address == right.Address && left.Port == right.Port;
    }

    public static bool operator !=(IP4 left, IP4 right)
    {
      return !(left == right);
    }

    public static implicit operator IPEndPoint(IP4 ip)
    {
      return new IPEndPoint(IPAddress.Parse(ip.Address), ip.Port);
    }

    public static implicit operator IP4(IPEndPoint endPoint)
    {
      return new IP4(endPoint.Address.ToString(), endPoint.Port);
    }

    public static implicit operator IPAddress(IP4 ip)
    {
      return new IPAddress(long.Parse(ip.Address.Replace(".", "")));
    }

    public static implicit operator IP4(IPAddress ip)
    {
      return new IP4(ip.ToString(), -1);
    }

    public bool Equals(IP4 other)
    {
      return string.Equals(Address, other.Address) && Port == other.Port;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      return obj is IP4 ip4 && Equals(ip4);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((Address != null ? Address.GetHashCode() : 0) * 397) ^ Port;
      }
    }
  }
}
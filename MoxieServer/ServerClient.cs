using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;

namespace Moxie.Server
{
  public class ServerClient
  {
    public readonly string name;
    public readonly IPAddress ip;
    public readonly int port;
    public readonly int id;

    public int attempt;

    public ServerClient(string name, IPAddress ip, int port, int id)
    {
      this.name = name;
      this.ip = ip;
      this.port = port;
      this.id = id;
    }
  }
}
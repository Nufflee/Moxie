using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using Moxie.Common;

namespace Moxie.Server
{
  public class ServerClient
  {
    public readonly string name;
    public readonly IP4 ip;
    public readonly string id;

    public int attempt;

    public ServerClient(string name, IP4 ip, string id)
    {
      this.name = name;
      this.ip = ip;
      this.id = id;
    }
  }
}
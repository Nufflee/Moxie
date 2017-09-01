using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moxie
{
  public class Client
  {
    string name;
    string address;
    int port;
    ClientWindow window;

    UdpClient client;
    IPAddress ip;

    Thread send;

    public Client(string name, string address, int port, ClientWindow window)
    {
      this.name = name;
      this.address = address;
      this.port = port;
      this.window = window;

      if (!OpenConnection(address, port))
      {
        Print("Connection failed!");
        return;
      }

      Print("Attempting a connection to " + address + ":" + port + ", user: " + name);
    }

    bool OpenConnection(string address, int port)
    {
      client = new UdpClient(port);
      ip = IPAddress.Parse(address);

      return true;
    }

    string Recieve()
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
      byte[] data = client.Receive(ref endPoint);

      return Encoding.ASCII.GetString(data);
    }

    void Send(byte[] data)
    {
      send = new Thread(() =>
      {
        client.Send(data, data.Length, new IPEndPoint(ip, port));
      });

      send.Start();
    }

    void Print(string message) => window.Print(message);
  }
}
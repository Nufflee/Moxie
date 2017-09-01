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

      if (!OpenConnection(address))
      {
        Print("Connection failed!");
        return;
      }

      Print("Attempting a connection to " + address + ":" + port + ", user: " + name);
      Send($"{name} connected from {address}:{port}".GetBytes());
    }

    public void SendMessage(string message)
    {
      if (string.IsNullOrWhiteSpace(message))
        return;

      message = message.TrimStart(' ');
      message = message.TrimEnd(' ');
      message = $"{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().Prepend("0", (text) => 2 - text.Length)} {name}: {message}";

      window.ShowMessage(message);
      Send(message.GetBytes());
    }

    bool OpenConnection(string address)
    {
      client = new UdpClient();
      ip = IPAddress.Parse(address);

      return true;
    }

    string Recieve()
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
      byte[] data = client.Receive(ref endPoint);

      return data.GetString();
    }

    void Send(byte[] data)
    {
      send = new Thread(() =>
      {
        client.Send(data, data.Length, new IPEndPoint(ip, port));
        Console.WriteLine(((IPEndPoint)client.Client.LocalEndPoint).Port);
      });

      send.Start();
    }

    void Print(string message) => window.Print(message);
  }
}
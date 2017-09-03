using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Moxie.Common;
using Moxie.Server;

namespace Moxie
{
  public class Client
  {
    string name;
    IP4 ip;
    IP4 serverIp;
    ClientWindow window;

    UdpClient client;

    Thread send;

    public Client(string name, IP4 serverIp, ClientWindow window)
    {
      this.name = name;
      this.window = window;
      this.serverIp = serverIp;

      if (!OpenConnection())
      {
        Print("Connection failed!");
        return;
      }

      Print("Attempting a connection to " + serverIp.Address + ":" + serverIp.Port + ", user: " + name);
    }

    public void SendMessage(string message)
    {
      if (string.IsNullOrWhiteSpace(message))
        return;

      message = message.TrimStart(' ');
      message = message.TrimEnd(' ');
      string fullMessage = $"{DateTime.Now.ToShortTimeString()} {name}: {message}";

      window.ShowMessage(fullMessage);

      Send(new MessagePacket(name, message));
    }

    bool OpenConnection()
    {
      client = new UdpClient(0);
      client.Connect(serverIp.Address, serverIp.Port);
      ip = (IP4)client.Client.LocalEndPoint;

      Send(new ConnectionPacket(name, ip));

      return true;
    }

    string Recieve()
    {
      IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverIp.Port);
      byte[] data = client.Receive(ref endPoint);

      return data.GetString();
    }

    void Send(Packet packet)
    {
      send = new Thread(() =>
      {
        byte[] data = packet.Serialize();

        client.Send(data, data.Length);
      });

      send.Start();
    }

    void Print(string message) => window.Print(message);
  }
}
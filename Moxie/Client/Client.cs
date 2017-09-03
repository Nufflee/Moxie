using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Threading;
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

    Thread send, recieve;

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

      Recieve();
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

    void Recieve()
    {
      recieve = new Thread(() =>
      {
        while (true)
        {
          IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverIp.Port);
          byte[] data = client.Receive(ref endPoint);

          Process(data);
        }
      });

      recieve.Start();
    }

    void Process(byte[] data)
    {
      object packetData = null;

      try
      {
        packetData = Packet.Deserialize(data);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }

      if (packetData is ConnectionPacket)
      {
      }
      else if (packetData is MessagePacket)
      {
        MessagePacket packet = (MessagePacket)packetData;

        Dispatcher.CurrentDispatcher.Invoke(() => ShowMessage(packet.ToString()));
      }
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
    void ShowMessage(string message) => window.ShowMessage(message);
  }
}
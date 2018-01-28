using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;
using Moxie.Client.UI;
using Moxie.Common;
using Moxie.Server.Packets;

namespace Moxie.Client
{
  public class Client
  {
    private string name;
    private IP4 ip;
    private IP4 serverIp;
    private ClientWindow window;

    private User user;

    private UdpClient client;

    private Thread send, recieve;

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

      Send(new MessagePacket(user, message));
    }

    private bool OpenConnection()
    {
      client = new UdpClient(0);
      client.Connect(serverIp.Address, serverIp.Port);
      ip = (IP4)client.Client.LocalEndPoint;

      user = new User(name, Guid.NewGuid(), ip);

      Send(new ConnectionPacket(user));

      return true;
    }

    private void Recieve()
    {
      recieve = new Thread(() =>
      {
        while (true)
        {
          IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverIp.Port);
          byte[] data = null;

          try
          {
            data = client.Receive(ref endPoint);
          }
          catch (SocketException)
          {
          }

          Process(data);
        }
      });

      recieve.Start();
    }

    private void Process(byte[] data)
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

      switch (packetData)
      {
        case TextPacket packet:
          Dispatcher.CurrentDispatcher.Invoke(() => ShowMessage(packet.FormattedText));

          break;

        case ListPacket<TextPacket> packet:
          foreach (TextPacket message in packet.Packets)
          {
            Dispatcher.CurrentDispatcher.Invoke(() => ShowMessage(message.FormattedText));
          }

          break;
      }
    }

    private void Send(Packet packet)
    {
      send = new Thread(() =>
      {
        byte[] data = packet.Serialize();

        client.Send(data, data.Length);
      });

      send.Start();
    }

    private void Print(string message)
    {
      window.Print(message);
    }

    private void ShowMessage(string message)
    {
      window.ShowMessage(message);
    }
  }
}
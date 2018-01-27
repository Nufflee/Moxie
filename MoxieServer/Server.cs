using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Moxie.Common;
using Moxie.Server.Packets;

namespace Moxie.Server
{
  public class Server
  {
    private int port;
    private UdpClient udpClient;

    private Thread run, manage, send, recieve;

    private List<User> users = new List<User>();
    private List<TextPacket> texts = new List<TextPacket>();

    private bool isRunning;

    public Server(int port)
    {
      this.port = port;
      udpClient = new UdpClient(port);

      run = new Thread(Run);
      run.Start();
    }

    private void Run()
    {
      isRunning = true;

      Console.WriteLine($"Server started on port: {port}");

      ManageClients();
      Recieve();
    }

    private void ManageClients()
    {
      manage = new Thread(() =>
      {
        while (isRunning)
        {
        }
      });

      manage.Start();
    }

    private void Recieve()
    {
      recieve = new Thread(() =>
      {
        while (isRunning)
        {
          IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

          byte[] data = udpClient.Receive(ref endPoint);

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
        case ConnectionPacket packet:
          users.Add(packet.User);
          Send(new ListPacket<TextPacket>(texts, udpClient.Client.LocalEndPoint as IPEndPoint), packet.Sender);
          SendToAll(new TextPacket($"{packet.User.Name} joined! Say hello.", packet.User.Ip), false);

          Console.WriteLine($"{packet.User.Name} connected to the server with id: {packet.User.Id}");

          break;

        case TextPacket packet:
          SendToAll(packet, false);
          texts.Add(packet);

          if (texts.Count >= 100)
          {
            texts.RemoveRange(100, texts.Count - 100);
          }

          Console.WriteLine(packet.FormattedText);

          break;
      }
    }

    private void SendToAll(Packet packet, bool sendToSender = true)
    {
      foreach (User user in users)
      {
        if (!sendToSender)
        {
          if (user.Ip != packet.Sender)
          {
            Send(packet, user.Ip);
          }
        }
        else
        {
          Send(packet, user.Ip);
        }
      }
    }

    private void Send(Packet packet, IP4 ip)
    {
      send = new Thread(() =>
      {
        byte[] data = packet.Serialize();

        udpClient.Send(data, data.Length, ip);
      });

      send.Start();
    }
  }
}